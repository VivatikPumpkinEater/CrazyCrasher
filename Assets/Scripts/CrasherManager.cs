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
    [SerializeField] private CrasherController _currentCrasher = null;

    [Header("Upgrade")] [SerializeField] private UpgradeButtons _upgradeButtons = null;

    [SerializeField] private UpgradeInfo _upgradeInfo = null;

    [Header("OtherSettings")] [SerializeField]
    private Joystick _joystick = null;

    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera = null;

    private Transform _crasherPlace = null;

    private int _currentLvl = 0;
    private int _sizeLvl = 0;
    private int _powerLvl = 0;

    private void Awake()
    {
        _fuel.SetFuel(0.25f);

        _crasherPlace = this.transform;

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
                UpgradePower();
                break;
            case UpgradeType.Size:
                UpgradeSize();
                break;
        }
    }

    private void UpgradeSize()
    {
        if (_sizeLvl + 1 != _upgradeInfo.SizeLvls.Count)
        {
            _saw.transform.DOScale(_saw.transform.localScale + _upgradeInfo.SizeLvls[_sizeLvl].StepSize, 0.2f);
            _sizeLvl++;
        }

        if (_sizeLvl == _upgradeInfo.SizeLvls.Count - 1)
        {
            Debug.Log("MaxLvlSize");
            _upgradeButtons.UpgradeSize.interactable = false;
        }
    }

    private void UpgradePower()
    {
        if (_powerLvl + 1 != _upgradeInfo.PowerLvls.Count)
        {
            _saw.LevelUp(_upgradeInfo.PowerLvls[_powerLvl].SpeedMotor, _upgradeInfo.PowerLvls[_powerLvl].Damage);
            _powerLvl++;
        }

        if (_powerLvl == _upgradeInfo.PowerLvls.Count - 1)
        {
            Debug.Log("MaxLvlPower");
            _upgradeButtons.UpgradePower.interactable = false;
        }
    }

    private void UpgradeCrasher()
    {
        if (_currentLvl + 1 != _crashersLvl.Count)
        {
            SpawnCrasher();

            _currentLvl++;
        }

        if (_currentLvl == _crashersLvl.Count - 1)
        {
            Debug.Log("MaxLvlLength");
            _upgradeButtons.UpgradeLength.interactable = false;
        }
    }

    private void SpawnCrasher()
    {
        if (_currentCrasher != null)
        {
            _saw.transform.parent = null;
            Destroy(_currentCrasher.gameObject);
        }

        _currentCrasher = Instantiate(_upgradeInfo.LengthLvls[_currentLvl].Crasher, _crasherPlace);
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