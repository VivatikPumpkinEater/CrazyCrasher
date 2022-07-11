using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SawController : MonoBehaviour
{
    [SerializeField] private float _power = 100f;
    [SerializeField] private float _damage = 10f;

    [SerializeField] private Saw _saw = null;


    private void Awake()
    {
        _saw.Damage = _damage;
    }
    
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, -_power * Time.deltaTime));
    }

    public void LevelUp(float power, float damage)
    {
        _power = power;
        _damage = damage;
    }

}