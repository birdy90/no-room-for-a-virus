using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Units
{
    [RequireComponent(typeof(HealthController))]
    public class StatsController : MonoBehaviour
    {
        public UnitStats Stats => _currentStats;
        
        public UnitStats BaseStats;
        
        [SerializeField] private GameObject BonusIconsList;
        [SerializeField] private BonusGameUIIcon BonusIconPrefab;
        [SerializeField] private TextMeshProUGUI StatsDisplayedText;

        private HealthController _healthController;
        private UnitStats _weaponStats;
        private readonly List<UnitStats> _bonusStats = new List<UnitStats>();
        private UnitStats _currentStats;

        private void Awake()
        {
            Recalculate();
        }

        private void OnDestroy()
        {
            OnStatsUpdated.RemoveAllListeners();
        }

        public void SetWeaponStats(UnitStats stats)
        {
            _weaponStats = stats;
            Recalculate();
        }

        public void AddBonus(UnitStats bonus)
        {
            _bonusStats.Add(bonus);
            Recalculate();
            OnStatsUpdated?.Invoke();
        }

        private void Recalculate()
        {
            _currentStats = Instantiate(BaseStats);

            if (_weaponStats)
            {
                _currentStats += _weaponStats;
            }

            foreach (UnitStats stats in _bonusStats)
            {
                _currentStats += stats;
            }
            
            UpdateUI();
        }

        private void UpdateUI()
        {
            UpdateBonusIcons();
            UpdateStatsDetails();
        }

        private void UpdateBonusIcons()
        {
            if (!BonusIconsList) return;
            
            foreach (Transform childIcon in BonusIconsList.transform)
            {
                Destroy(childIcon.gameObject);
            }

            Dictionary<string, Tuple<UnitStats, int>> mergedStats = new Dictionary<string, Tuple<UnitStats, int>>();
            foreach (UnitStats bonus in _bonusStats)
            {
                if (!mergedStats.ContainsKey(bonus.Name))
                {
                    mergedStats.Add(bonus.Name, new Tuple<UnitStats, int>( bonus, 1));
                }
                else
                {
                    mergedStats[bonus.Name] = new Tuple<UnitStats, int>(bonus, mergedStats[bonus.Name].Item2 + 1);
                }
            }

            foreach (Tuple<UnitStats, int> item in mergedStats.Values)
            {
                UnitStats bonus = item.Item1;
                int number = item.Item2;
                BonusGameUIIcon icon = Instantiate(BonusIconPrefab, BonusIconsList.transform);
                icon.SetBonus(bonus, number);
            }
            
        }

        private void UpdateStatsDetails()
        {
            if (!StatsDisplayedText) return;

            StringBuilder statsSb = new StringBuilder();
            statsSb.Append($"Damage: {_currentStats.Damage:F1}\n");
            statsSb.Append($"Reload time: {_currentStats.ReloadTime:F2}s\n");
            statsSb.Append($"Speed: {_currentStats.Speed:F1}\n");
            statsSb.Append($"Damage reduction: {_currentStats.DamageReduction:F1}\n");
            statsSb.Append($"Temporal shield: {_currentStats.TemporalShield:F1}");
            StatsDisplayedText.text = statsSb.ToString();
        }
        
        public UnityEvent OnStatsUpdated;
    }
}