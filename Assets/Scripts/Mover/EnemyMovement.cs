using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemySquad Squad { get; set; }
    private EnemyAnimator _animator;
    private Vector3 _direction = Vector3.zero;

    private void Awake()
    {
        if (!gameObject.TryGetComponent<EnemyAnimator>(out EnemyAnimator anim))
        {
            Debug.Log("Missing EnemyAnimator Script!");
            anim = gameObject.AddComponent<EnemyAnimator>();
        }
        _animator = anim;
    }

    private void Update()
    {
        UpdateDirection();
        Move();
    }

    private void UpdateDirection()
    {
        Vector3 playerPosition = PlayerManager.Instance.transform.position;
        _direction = playerPosition - transform.position;
        transform.LookAt(playerPosition);
    }

    private void Move()
    {
        if (_direction.magnitude < 5f)
        {
            _animator.EnableRun();
            _direction.Normalize();
            transform.position += EnemyManager.Instance.Speed * Time.deltaTime * _direction;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Player") && !Squad.InCollision)
        {
            Squad.RecomputeSquad();
        }
    }
}
