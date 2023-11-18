using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    [field: SerializeField] public float HitPoints = 100f;
    [SerializeField] private Color _damageColor;
    [SerializeField] private float _damageFXTime = .5f;
    [SerializeField] private Renderer _renderer;

    private Color _initialColor;
    private Coroutine _fxCoroutine;

    private void Start()
    {
        _initialColor = _renderer.material.color;
    }

    public void Hit(GameObject hitter, float damage)
    {
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        if (_fxCoroutine != null)
        {
            StopCoroutine(_fxCoroutine);
        }

        _fxCoroutine = StartCoroutine(TakeDamageCoroutine());
        HitPoints -= damage;

        if (HitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator TakeDamageCoroutine()
    {
        _renderer.material.color = _damageColor;

        yield return new WaitForSeconds(_damageFXTime);

        _renderer.material.color = _initialColor;
    }

}
