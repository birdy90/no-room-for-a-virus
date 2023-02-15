using Units;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon: MonoBehaviour
    {
        public static string FireAnimationTrigger = "Fire";

        public UnitStats Stats;

        public abstract void Fire(UnitStats damagerStats);
    }
}