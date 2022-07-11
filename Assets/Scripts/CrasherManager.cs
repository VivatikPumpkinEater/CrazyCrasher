using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;

public class CrasherManager : MonoBehaviour
{
    [Header("SawSettings")] [SerializeField]
    private SawController _saw = null;
    [SerializeField] private Vector3 _sizeStep;

    [Header("CrasherSettings")] [SerializeField]
    private List<CrasherController> _crashersLvl = new List<CrasherController>();

    [Space(25)] [SerializeField] private Fuel _fuel = null;
    [SerializeField] private Transform _crasherPlace = null;
    [SerializeField] private CrasherController _currentCrasher = null;

    [Header("UpgradeButtons")] [SerializeField]
    private UpgradeButtons _upgradeButtons = null;

    [Header("OtherSettings")] [SerializeField]
    private Joystick _joystick = null;

    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera = null;

    private int _currentLvl = 0;

    private void Awake()
    {
        _fuel.SetFuel(1f);
        
        InitializeButtons();
        
        SpawnCrasher();
    }

    private void InitializeButtons()
    {
        _upgradeButtons.UpgradeLength.onClick.AddListener(() => Upgrade(UpgradeType.Length));
        _upgradeButtons.UpgradeFuel.onClick.AddListener(() => Upgrade(UpgradeType.Fuel));
        _upgradeButtons.UpgradePower.onClick.AddListener(() => Upgrade(UpgradeType.Power));
        _upgradeButtons.UpgradeSize.onClick.AddListener(() => Upgrade(UpgradeType.Size));
    }

    private void UpgradeCrasher()
    {
        if (_currentLvl + 1 != _crashersLvl.Count)
        {
            _currentLvl++;

            SpawnCrasher();
        }
        else
        {
            Debug.Log("MaxLvl");
            _upgradeButtons.UpgradeLength.interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradeCrasher();
        }
    }
    
    private void Upgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Length:
                UpgradeCrasher();
                break;
            case UpgradeType.Fuel:
                break;
            case UpgradeType.Power:
                break;
            case UpgradeType.Size:
                _saw.transform.DOScale(_saw.transform.localScale + _sizeStep, 0.2f);
                break;
        }
    }

    private void SpawnCrasher()
    {
        if (_currentCrasher != null)
        {
            _saw.transform.parent = null;
            Destroy(_currentCrasher.gameObject);
        }

        _currentCrasher = Instantiate(_crashersLvl[_currentLvl], _crasherPlace);
        _currentCrasher.JoystickInfo = _joystick;

        _saw.transform.parent = _currentCrasher.SawPosition;
        _saw.transform.localPosition = Vector3.zero;

        _cinemachineVirtualCamera.Follow = _currentCrasher.SawPosition;
    }
}

public enum UpgradeType
{
    Power,
    Fuel,
    Size,
    Length
}