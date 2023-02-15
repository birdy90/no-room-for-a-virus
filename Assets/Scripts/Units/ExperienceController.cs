using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    [RequireComponent(typeof(StatsController))]
    public class ExperienceController : MonoBehaviour
    {
        private float ExperienceForNextLevel => ExperienceCurveMultiplier * ((_currentLevel - 1) / 3 + 1);
        
        [SerializeField] private float ExperienceCurveMultiplier = 10f;
        [SerializeField] private Image ExperienceBar;

        private StatsController _unitStats;
        private float _currentLevel = 1f;
        private float _currentExperience = 1f;

        private void Start()
        {
            _unitStats = GetComponent<StatsController>();
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (ExperienceBar)
            {
                ExperienceBar.fillAmount = _currentExperience / ExperienceForNextLevel;
            }
        }

        public void AddExperience(float experience)
        {
            _currentExperience += experience;
            
            if (_currentExperience > ExperienceForNextLevel)
            {
                _currentExperience -= ExperienceForNextLevel;
                _currentLevel += 1;
            }
            
            UpdateUI();
        }
    }
}