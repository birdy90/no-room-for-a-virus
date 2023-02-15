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
        [SerializeField] private Image HealthBar;
        [SerializeField] private TextMeshProUGUI HealthText;

        private StatsController _unitStats;
        private float _currentHealth = 1f;

        private void Awake()
        {
            _unitStats = GetComponent<StatsController>();
        }

        private void Start()
        {
            _currentHealth = _unitStats.Stats.Health;
            UpdateUI();
        }

        private void TakeDamage(DamageSource damageSource)
        {
            float dealtDamage = damageSource.DamagerStats.Damage;

            _currentHealth = Mathf.Max(_currentHealth - dealtDamage, 0);
            UpdateUI();

            if (_currentHealth == 0 && TryGetComponent(out IMortal dieController))
            {
                dieController.Die();
            }
        }
    
        private void UpdateUI()
        {
            if (HealthBar)
            {
                HealthBar.fillAmount = _currentHealth / _unitStats.Stats.Health;
            }

            if (HealthText)
            {
                HealthText.text = $"{_currentHealth}/{_unitStats.Stats.Health}";
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            int colliderLayerMask = 1 << collision.gameObject.layer;
            if ((DamagingLayers & colliderLayerMask) == 0)
            {
                return;
            }
        
            if (collision.gameObject.TryGetComponent(out DamageSource damageSource))
            {
                TakeDamage(damageSource);
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