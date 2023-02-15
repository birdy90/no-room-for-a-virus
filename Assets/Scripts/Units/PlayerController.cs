using System;
using Interfaces;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(StatsController))]
    [RequireComponent(typeof(WeaponController))]
    public class PlayerController : MonoBehaviour, IMortal
    {
    
        private Rigidbody _rigidbody;

        private readonly Vector3 _forwardDirection = new Vector3(-1, 0, 1);
        private readonly Vector3 _sideDirection = new Vector3(1, 0, 1);
        private StatsController _statsController;
        private WeaponController _weaponController;
    
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _statsController = GetComponent<StatsController>();
            _weaponController = GetComponent<WeaponController>();
        }

        public void Move(float forwardMovement, float sideMovement)
        {
            _rigidbody.velocity = _statsController.Stats.Speed * (forwardMovement * _forwardDirection + sideMovement * _sideDirection).normalized;
        }

        public void LookAt(Vector3 lookAtPoint)
        {
            Vector3 playerLookAtPoint = lookAtPoint - new Vector3(0, Constants.WeaponVecticalPosition, 0);
            transform.LookAt(playerLookAtPoint);

            _weaponController.LookAt(lookAtPoint);
        }

        public void Fire()
        {
            _weaponController.Fire(_statsController.Stats);
        }

        public void Die()
        {
            Debug.Log("YOU DIED");
        }
    }
}
