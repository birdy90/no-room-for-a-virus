using Units;
using UnityEngine;

/// <summary>
/// There are two types of damage source:
/// 1. when damage source is on unit - then it should get DamagerStats from
/// unit it is connected with
/// 2. when damage source is on projectille - in that case we need
/// to manually set stats when instantiating projectiles
/// </summary>
[RequireComponent(typeof(Collider))]
public class DamageSource : MonoBehaviour
{
    public UnitStats DamagerStats;

    private void Awake()
    {
        if (TryGetComponent(out StatsController statsController))
        {
            DamagerStats = statsController.Stats;
        }
    }
}