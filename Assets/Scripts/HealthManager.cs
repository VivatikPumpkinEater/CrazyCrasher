using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthManager : MonoBehaviour
{
    private float _health = 10;

    private Rigidbody _rigidbody = null;

    private Rigidbody _rb
    {
        get => _rigidbody = _rigidbody ?? GetComponent<Rigidbody>();
    }

    private bool _die = false;
    
    public void Hit(float damage)
    {
        if(!_die)
        {
            _health -= damage * Time.deltaTime;

            if (_health <= 0)
            {
                _die = true;
                Die();
            }
        }
    }


    private void Die()
    {
        _die = true;
        
        LvlStatus.Instance.ProgressLvl();
        
        gameObject.layer = LayerMask.NameToLayer("Unconnected");
        _rb.isKinematic = false;
    }
}