using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon: MonoBehaviour
{
    public static string FireAnimationTrigger = "Fire";
    
    [SerializeField] protected float Damage;
    [SerializeField] protected float ReloadTime;
    
    private bool _isOnCooldown = false;
    private readonly float _lookAtTresholdDistance = 2f;

    private void OnDestroy()
    {
        StopCoroutine(nameof(ResetCooldown));
    }
    
    protected abstract void PerformFire();
    
    public void LookAt(Vector3 lookAtPoint)
    {
        Transform weaponTransform = transform;
        
        if ((lookAtPoint - weaponTransform.position).magnitude > _lookAtTresholdDistance)
        {
            weaponTransform.LookAt(lookAtPoint);
        }
        else
        {
            weaponTransform.rotation = weaponTransform.parent.rotation;
        }
    }

    public void Fire()
    {
        if (_isOnCooldown)
        {
            return;
        }
        
        PerformFire();
        
        _isOnCooldown = true;
        StartCoroutine(nameof(ResetCooldown));
    }

    public IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(ReloadTime);
        _isOnCooldown = false;
    }
}