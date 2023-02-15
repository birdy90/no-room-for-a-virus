using Interfaces;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(StatsController))]
    [RequireComponent(typeof(DamageSource))]
    public class Enemy : MonoBehaviour, IMortal
    {
        [SerializeField] private int ChildrenMin = 0;
        [SerializeField] private int ChildrenMax = 0;
        [SerializeField] private GameObject ChildPrefab;

        [SerializeField] private float SizeScale = 1f;
        
        private Rigidbody _rigidBody;
        private StatsController _statsController;
        private DamageSource _damageSource;
        private Transform _playerTransform;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _statsController = GetComponent<StatsController>();
            _damageSource = GetComponent<DamageSource>();
        }

        private void FixedUpdate()
        {
            MoveToPlayer();
            LookAtPlayer();
        }

        private void MoveToPlayer()
        {
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            _rigidBody.velocity = _statsController.Stats.Speed * direction;
        }

        private void LookAtPlayer()
        {
            transform.LookAt(_playerTransform.position);
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}