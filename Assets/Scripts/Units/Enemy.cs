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
        private PlayerController _playerController;
        private ExperienceController _experienceController;
        private Transform _playerTransform;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _statsController = GetComponent<StatsController>();
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

        public void SetPlayerController(PlayerController player)
        {
            _playerController = player;
            _playerTransform = _playerController.transform;
            _experienceController = player.GetComponent<ExperienceController>();
        }

        public void Die()
        {
            _experienceController.AddExperience(_statsController.Stats.Experience);
            Destroy(gameObject);
        }
    }
}