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

}