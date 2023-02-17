using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    [RequireComponent(typeof(StatsController))]
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private LayerMask DamagingLayers;
        [SerializeField] private float ShieldRestorationTime = 3f;
        [SerializeField] private Image HealthBar;
        [SerializeField] private TextMeshProUGUI HealthText;
        [SerializeField] private Image ShieldBar;
        [SerializeField] private TextMeshProUGUI ShieldText;

        [SerializeField] private bool HideHealthWhenFull = false;

        private StatsController _statsController;
        private float _currentHealth = 0f;
        private float _currentShield = 0f;
        private float _nextShieldRestoreStep = 0f;

        private Dictionary<GameObject, Coroutine> _activeDamagers = new Dictionary<GameObject, Coroutine>();

        private void Start()
        {
            _statsController = GetComponent<StatsController>();
            _statsController.OnStatsUpdated.AddListener(UpdateUI);
            
            _currentHealth = _statsController.Stats.Health;
            HealthBar.transform.parent.gameObject.SetActive(!HideHealthWhenFull);
            UpdateUI();

            if (ShieldBar)
            {
                StartCoroutine(nameof(RestoreShield));
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private void TakeDamage(DamageSource damageSource)
        {
            float dealtDamage = Mathf.Max(damageSource.DamagerStats.Damage - _statsController.Stats.DamageReduction, 0);

            if (dealtDamage <= _currentShield)
            {
                _currentShield -= dealtDamage;
                _nextShieldRestoreStep -= dealtDamage;
            }
            else
            {
                dealtDamage -= _currentShield;
                _currentShield = 0;
                _nextShieldRestoreStep = 0;
                _currentHealth = Mathf.Max(_currentHealth - dealtDamage, 0);
            }

            HealthBar.transform.parent.gameObject.SetActive(true);
            UpdateUI();

            if (_currentHealth == 0 && TryGetComponent(out IMortal dieController))
            {
                dieController.Die();
            }
        }
    
        private void UpdateUI()
        {
            UpdateHealthUI();
            UpdateShieldUI();
        }

        private void UpdateHealthUI()
        {
            if (HealthBar)
            {
                HealthBar.fillAmount = _currentHealth / _statsController.Stats.Health;
            }

            if (HealthText)
            {
                HealthText.text = $"{_currentHealth:F1}/{_statsController.Stats.Health:F1}";
            }
        }

        private void UpdateShieldUI()
        {
            if (!ShieldBar || !ShieldText || _statsController.Stats.TemporalShield <= 0) return;

            ShieldBar.transform.parent.gameObject.SetActive(true);
            ShieldBar.fillAmount = _currentShield / _statsController.Stats.TemporalShield;
            ShieldText.text = $"{_currentShield:F1}/{_statsController.Stats.TemporalShield:F1}";
        }

        private void OnCollisionEnter(Collision collision)
        {
            int colliderLayerMask = 1 << collision.gameObject.layer;
            if ((DamagingLayers & colliderLayerMask) == 0) return;

            if (collision.gameObject.TryGetComponent(out DamageSource damageSource))
            {
                Coroutine coroutine = StartCoroutine(nameof(CollisionStay), collision);
                _activeDamagers.Add(collision.gameObject, coroutine);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!_activeDamagers.ContainsKey(collision.gameObject)) return;
            CancelTakingDamage(collision);
        }

        private void CancelTakingDamage(Collision collision)
        {
            StopCoroutine(_activeDamagers[collision.gameObject]);
            _activeDamagers.Remove(collision.gameObject);   
        }

        private IEnumerator CollisionStay(Collision collision)
        {
            DamageSource damageSource = collision.gameObject.GetComponent<DamageSource>();

            try
            {
                while (collision.gameObject)
                {
                    TakeDamage(damageSource);
                    yield return new WaitForSeconds(1f);
                }
            }
            finally
            {
                CancelTakingDamage(collision);
            }
        }

        private IEnumerator RestoreShield()
        {
            float lerpDuration = 0f;
            
            while (true)
            {
                yield return new WaitForSeconds(ShieldRestorationTime - lerpDuration);
                lerpDuration = 0;
                    
                if (Math.Abs(_statsController.Stats.TemporalShield - _currentShield) > 0.001f)
                {
                    _nextShieldRestoreStep = Mathf.Min(_currentShield + 1, _statsController.Stats.TemporalShield);
                    while (Math.Abs(_nextShieldRestoreStep - _currentShield) > 0.001f)
                    {
                        lerpDuration += Time.deltaTime;
                        _currentShield = Mathf.Lerp(_currentShield, _nextShieldRestoreStep, 0.075f);
                        UpdateShieldUI();
                        yield return null;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            int colliderLayerMask = 1 << other.gameObject.layer;
            if ((DamagingLayers & colliderLayerMask) == 0)
            {
                return;
            }

            if (other.gameObject.TryGetComponent(out DamageSource damageSource))
            {
                TakeDamage(damageSource);
            }
        }
    }
}