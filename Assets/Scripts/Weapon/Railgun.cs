using UnityEngine;

public class Railgun : Weapon
{
    [SerializeField] private LineRenderer RailLinePrefab;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    protected override void PerformFire()
    {
        LineRenderer railline = Instantiate(RailLinePrefab, transform);
        railline.transform.SetParent(null);
    }
}