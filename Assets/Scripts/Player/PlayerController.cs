using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 1f;
    [SerializeField] private Weapon Weapon;
    
    private Rigidbody _rigidbody;

    private readonly Vector3 _forwardDirection = new Vector3(-1, 0, 1);
    private readonly Vector3 _sideDirection = new Vector3(1, 0, 1);
    private Weapon _weaponComponent;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _weaponComponent = Instantiate(Weapon, transform);
        _weaponComponent.transform.position = new Vector3(0.2f, Constants.WeaponVecticalPosition, 0.15f);
    }

    public void Move(float forwardMovement, float sideMovement)
    {
        _rigidbody.velocity = Speed * (forwardMovement * _forwardDirection + sideMovement * _sideDirection).normalized;
    }

    public void LookAt(Vector3 lookAtPoint)
    {
        Vector3 playerLookAtPoint = lookAtPoint - new Vector3(0, Constants.WeaponVecticalPosition, 0);
        transform.LookAt(playerLookAtPoint);

        _weaponComponent.LookAt(lookAtPoint);
    }

    public void Fire()
    {
        _weaponComponent.Fire();
    }
}
