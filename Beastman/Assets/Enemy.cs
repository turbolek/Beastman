using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SeekRange _seekRange;
    [SerializeField] private Hitter _hitter;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _attackInterval = 2f;

    private PlayerController _target;
    private float _attackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        _seekRange.Init(OnPlayerEnteredRange, OnPlayerExitedRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            return;
        }


        if (_attackCooldown <= 0)
        {
            _attackCooldown = _attackInterval;
            _hitter.Hit();
        }
        else
        {
            _attackCooldown -= Time.deltaTime;
            HandleMovement();
        }
    }

    private void OnPlayerEnteredRange(PlayerController player)
    {
        _target = player;
    }

    private void OnPlayerExitedRange(PlayerController player)
    {
        if (player == _target)
        {
            _target = null;
        }
    }

    private void HandleMovement()
    {
        transform.LookAt(_target.transform);
        var direction = (_target.transform.position - transform.position).normalized;

        transform.Translate(direction * _speed * Time.deltaTime);
    }
}
