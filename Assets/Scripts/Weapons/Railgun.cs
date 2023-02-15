using Units;
using UnityEngine;

namespace Weapons
{
    public class Railgun : Weapon
    {
        [SerializeField] private DamageSource RailLinePrefab;
    
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    
        public override void Fire(UnitStats damagerStats)
        {
            DamageSource railline = Instantiate(RailLinePrefab, transform);
            railline.transform.SetParent(null);
            railline.DamagerStats = damagerStats;
        }
    }
}