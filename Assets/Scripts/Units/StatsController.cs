using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class StatsController : MonoBehaviour
    {
        public UnitStats Stats => _currentStats;
        
        [SerializeField] private UnitStats BaseStats;

        private UnitStats _weaponStats;
        private readonly List<UnitStats> _bonusStats = new List<UnitStats>();
        private UnitStats _currentStats;
        

        private void Awake()
        {
            Recalculate();
        }

        public void SetWeaponStats(UnitStats stats)
        {
            _weaponStats = stats;
            Debug.Log($"ADDED WEAPON: {_weaponStats}");
            Recalculate();
        }

        public void AddBonus(UnitStats bonus)
        {
            _bonusStats.Add(bonus);
            Recalculate();
        }

        private void RemoveBonus(UnitStats bonus)
        {
            _bonusStats.Remove(bonus);
            Recalculate();
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
        }
    }
}