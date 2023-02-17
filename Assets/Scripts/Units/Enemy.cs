using System;
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
        
        public UnitStats Stats => _statsController.Stats;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _statsController = GetComponent<StatsController>();
        }

        private void FixedUpdate()
        {
            MoveTowardsPlayer();
        }

        private void MoveTowardsPlayer()
        {
            Vector3 playerPosition = _playerTransform.position;
            Vector3 direction = (playerPosition - transform.position).normalized;
            _rigidBody.velocity = _statsController.Stats.Speed * direction;
            
            transform.LookAt(playerPosition);
        }

        public void SetPlayerController(PlayerController playerController)
        {
            _playerController = playerController;
            _playerTransform = _playerController.transform;
            _experienceController = playerController.GetComponent<ExperienceController>();
        }

        public void Die()
        {
            _experienceController.AddExperience(_statsController.Stats.Experience);
            Destroy(gameObject);
        }
    }
}