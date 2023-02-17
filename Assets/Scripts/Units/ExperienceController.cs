using System;
using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Units
{
    [RequireComponent(typeof(StatsController))]
    public class ExperienceController : MonoBehaviour
    {
        private float ExperienceForNextLevel => ExperienceCurveMultiplier * ((_currentLevel - 1) / 1.5f + 1);
        
        [SerializeField] private float ExperienceCurveMultiplier = 10f;
        [SerializeField] private Image ExperienceBar;
        [SerializeField] private TextMeshProUGUI LevelText;
        [SerializeField] private TextMeshProUGUI ExperienceText;
        [SerializeField] private BonusSelectionWindow BonusSelectionWindow;

        private StatsController _unitStats;
        private int _currentLevel = 1;
        private float _currentExperience;

        public int Level => _currentLevel;

        private void Start()
        {
            _unitStats = GetComponent<StatsController>();
            BonusSelectionWindow.OnBonusSelected += NewBonus;
            UpdateUI();
            ShowBonusSelectionWindow();
        }

        private void UpdateUI()
        {
            if (!ExperienceBar || !ExperienceText || !LevelText)
            {
                throw new Exception("Not enough interface to display experience");
            }
            
            ExperienceBar.fillAmount = _currentExperience / ExperienceForNextLevel;
            LevelText.text = $"Lvl. {_currentLevel}";
            ExperienceText.text = $"{_currentExperience:F0}/{ExperienceForNextLevel:F0}";
        }

        public void AddExperience(float experience)
        {
            _currentExperience += experience;
            
            if (_currentExperience > ExperienceForNextLevel)
            {
                _currentExperience -= ExperienceForNextLevel;
                _currentLevel += 1;
                StartCoroutine(nameof(ShowBonusSelectionWindowWithTimer));
            }
            
            UpdateUI();
        }
        
        private IEnumerator ShowBonusSelectionWindowWithTimer()
        {
            GameFlow.PauseMenuAllowed = false;
            yield return new WaitForSecondsRealtime(0.2f);
            ShowBonusSelectionWindow();
        }

        private void ShowBonusSelectionWindow()
        {
            GameFlow.Pause();
            BonusSelectionWindow.gameObject.SetActive(true);
            BonusSelectionWindow.FillBonusButtons();
        }

        private void NewBonus(UnitStats bonus)
        {
            _unitStats.AddBonus(bonus);
            GameFlow.PauseMenuAllowed = true;
        }
    }
}