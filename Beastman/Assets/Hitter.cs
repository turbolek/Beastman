using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitter : MonoBehaviour
{
    [SerializeField] private float hitSphereRadius = .5f;
    [SerializeField] private float hitSphereRange = .5f;
    [SerializeField] private float _damage = 35f;

    public void Hit()
    {
        var hits = Physics.SphereCastAll(transform.position, hitSphereRadius, transform.forward, hitSphereRange);
        foreach (var hit in hits)
        {
            Hittable hittable = hit.transform.GetComponent<Hittable>();
            if (hittable != null && hittable.gameObject != gameObject)
            {
                hittable.Hit(gameObject, _damage);
            }
        }
    }
}
