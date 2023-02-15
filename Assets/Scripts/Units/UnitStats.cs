using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "Stats", menuName = "Scriptable/Unit stats (base)")]
    public class UnitStats : ScriptableObject
    {
        public string Name;
        public Sprite StatsSprite;
        
        public float Speed;
        public float Damage;
        public float Health;
        public float TemporalShield;
        public float DamageReduction;
        public float ReloadTime;

        public float Experience;

        public static UnitStats operator +(UnitStats a, UnitStats b)
        {
            UnitStats stats = CreateInstance<UnitStats>();

            stats.Name = a.Name;
            stats.StatsSprite = a.StatsSprite;
            stats.Experience = a.Experience;
            
            stats.Speed = a.Speed + b.Speed;
            stats.Damage = a.Damage + b.Damage;
            stats.Health = a.Health + b.Health;
            stats.TemporalShield = a.TemporalShield + b.TemporalShield;
            stats.DamageReduction = a.DamageReduction + b.DamageReduction;
            stats.ReloadTime = a.ReloadTime + b.ReloadTime;

            return stats;
        }

        public override string ToString()
        {
            return $"{Speed}{Damage}{Health}{TemporalShield}{DamageReduction}{ReloadTime}";
        }
    }
}