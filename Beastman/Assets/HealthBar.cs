using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Hittable _hittable;
    [SerializeField] private Image _fillImage;

    private float _maxHP = 100f;

    private void Start()
    {
        _maxHP = _hittable.HitPoints;
    }

    private void Update()
    {
        _fillImage.fillAmount = _hittable.HitPoints / _maxHP;
    }

}
