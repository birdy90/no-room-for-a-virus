using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 1f;
    
    private Rigidbody _rigidbody;

    private readonly Vector3 _forwardDirection = new Vector3(-1, 0, 1);
    private readonly Vector3 _sideDirection = new Vector3(1, 0, 1);
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(float forwardMovement, float sideMovement)
    {
        _rigidbody.velocity = Speed * (forwardMovement * _forwardDirection + sideMovement * _sideDirection).normalized;
    }

    public void LookAt(Vector3 lookAtPoint)
    {
        transform.LookAt(lookAtPoint);
    }
}
