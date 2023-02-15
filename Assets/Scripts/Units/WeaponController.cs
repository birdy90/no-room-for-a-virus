using System.Collections;
using UnityEngine;
using Weapons;

namespace Units
{
    [RequireComponent(typeof(StatsController))]
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon DefaultWeapon;
        
        private Weapon _weaponComponent;
        private StatsController _statsController;

        private bool _isOnCooldown = false;
        private readonly float _lookAtTresholdDistance = 2f;

        private void Awake()
        {
            _statsController = GetComponent<StatsController>();
            SetWeapon(DefaultWeapon);
        }

        private void OnDestroy()
        {
            StopCoroutine(nameof(ResetCooldown));
        }

        public void LookAt(Vector3 lookAtPoint)
        {
            Transform weaponTransform = _weaponComponent.transform;
        
            if ((lookAtPoint - weaponTransform.position).magnitude > _lookAtTresholdDistance)
            {
                weaponTransform.LookAt(lookAtPoint);
            }
            else
            {
                weaponTransform.rotation = weaponTransform.parent.rotation;
            }
        }

        public void Fire(UnitStats damagerStats)
        {
            if (_isOnCooldown)
            {
                return;
            }
        
            _weaponComponent.Fire(damagerStats);
        
            _isOnCooldown = true;
            StartCoroutine(nameof(ResetCooldown));
        }

        public void SetWeapon(Weapon weaponPrefab)
        {
            if (!DefaultWeapon)
            {
                return;
            }

            _weaponComponent = Instantiate(weaponPrefab, transform);
            _weaponComponent.transform.position = new Vector3(0.2f, Constants.WeaponVecticalPosition, 0.15f);
            
            _statsController.SetWeaponStats(_weaponComponent.Stats);
        }

        public IEnumerator ResetCooldown()
        {
            yield return new WaitForSeconds(_statsController.Stats.ReloadTime);
            _isOnCooldown = false;
        }
    }
}