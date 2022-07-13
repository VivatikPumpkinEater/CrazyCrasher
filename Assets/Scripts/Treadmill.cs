using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField] private float _power = 100f;
    [SerializeField] private List<Transform> _miniRotators = new List<Transform>();

    private void Update()
    {
        foreach (var miniRotator in _miniRotators)
        {
            miniRotator.Rotate(new Vector3(0, 0, _power * Time.deltaTime));
        }
    }
}
