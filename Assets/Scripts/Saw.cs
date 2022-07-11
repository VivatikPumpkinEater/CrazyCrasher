using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private float _damage = 1;

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
    
    private void OnCollisionStay(Collision collision)
    {

        var cellHp = collision.collider.GetComponent<HealthManager>();

        if (cellHp)
        {
            cellHp.Hit(_damage);
        }
    }
}
