using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public float Damage = 1f;
    public float Health = 3f;
    public float Speed = 1f;
    public float Shield = 0f;
    public int ChildrenMin = 0;
    public int ChildrenMax = 0;
    public GameObject ChildPrefab;

    public float SizeScale = 1f;

    private Rigidbody _rigidBody;
    private Transform _playerTransform;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
        LookAtPlayer();
    }

    private void MoveToPlayer()
    {
        Vector3 direction = (_playerTransform.position - transform.position).normalized;
        _rigidBody.velocity = Speed * direction;
    }

    private void LookAtPlayer()
    {
        transform.LookAt(_playerTransform.position);
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }
}