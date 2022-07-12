using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    [SerializeField] private float _duration = 50f;
    private float _fuel = 0;
    private bool _game = false;

    private Image _image = null;

    private Image _fuelStats
    {
        get => _image = _image ?? GetComponent<Image>();
    }

    private void Start()
    {
        UIManager.Instance.StartGame += StartFuel;
    }

    private void StartFuel()
    {
        _game = true;
    }

    public void SetFuel(float value)
    {
        _fuelStats.fillAmount = value;
        _fuel = value * _duration;
        
        Debug.Log("StartFuel");
    }

    private void Update()
    {
        if (_game)
        {
            _fuel -= Time.deltaTime;

            _fuelStats.fillAmount = _fuel / _duration;

            if (_fuelStats.fillAmount <= 0)
            {
                _game = false;
                Debug.Log("GameOver");
            }
        }
    }
}