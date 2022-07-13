using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Animations.Rigging;

public class CrasherManager : MonoBehaviour
{
    [Header("SawSettings")] [SerializeField]
    private SawController _saw = null;


    [Header("CrasherSettings")] [SerializeField]
    private Transform _crasherPos = null;

    [Space(25)] [SerializeField] private Fuel _fuel = null;
    [SerializeField] private CrasherController _currentCrasher = null;

    [Header("Upgrade")] [SerializeField] private UpgradeButtons _upgradeButtons = null;

    [SerializeField] private UpgradeInfo _upgradeInfo = null;

    [Header("OtherSettings")] [SerializeField]
    private Joystick _joystick = null;

    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera = null;

    private Rig _crasherRig = null;

    private int _lengthLvl = 0;
    private int _fuelLvl = 0;
    private int _sizeLvl = 0;
    private int _powerLvl = 0;

    private void Awake()
    {
        _fuel.SetFuel(0.25f);


        InitializeButtons();

        SpawnCrasher();

        InitializedCost();
    }

    private void Start()
    {
        UIManager.Instance.StartGame += StartGame;
        UIManager.Instance.EndGame += EndGame;
    }

    private void InitializeButtons()
    {
        _upgradeButtons.UpgradeLength.onClick.AddListener(() => Upgrade(UpgradeType.Length));
        _upgradeButtons.UpgradeFuel.onClick.AddListener(() => Upgrade(UpgradeType.Fuel));
        _upgradeButtons.UpgradePower.onClick.AddListener(() => Upgrade(UpgradeType.Power));
        _upgradeButtons.UpgradeSize.onClick.AddListener(() => Upgrade(UpgradeType.Size));
    }

    private void InitializedCost()
    {
        _upgradeButtons.CostLength.text = _upgradeInfo.LengthLvls[_lengthLvl].CostUpgrade.ToString() + "$";
        _upgradeButtons.CostFuel.text = _upgradeInfo.FuelLvls[_fuelLvl].CostUpgrade.ToString() + "$";
        _upgradeButtons.CostPower.text = _upgradeInfo.PowerLvls[_powerLvl].CostUpgrade.ToString() + "$";
        _upgradeButtons.CostSize.text = _upgradeInfo.SizeLvls[_sizeLvl].CostUpgrade.ToString() + "$";
    }

    private void StartGame()
    {
        _crasherRig.weight = 1f;
    }

    private void EndGame()
    {
        _crasherRig.weight = 0f;
    }

    private void Upgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Length:
                UpgradeCrasher();
                break;
            case UpgradeType.Fuel:
                UpgradeFuel();
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
        if (_sizeLvl != _upgradeInfo.SizeLvls.Count &&
            GameManager.Instance.SpendMoney(_upgradeInfo.SizeLvls[_sizeLvl].CostUpgrade))
        {
            _saw.transform.DOScale(_saw.transform.localScale + _upgradeInfo.SizeLvls[_sizeLvl].StepSize, 0.2f);
            _sizeLvl++;
        }

        if (_sizeLvl == _upgradeInfo.SizeLvls.Count)
        {
            Debug.Log("MaxLvlSize");
            _upgradeButtons.UpgradeSize.interactable = false;

            _upgradeButtons.CostSize.text = "Max Lvl";
        }
        else
        {
            _upgradeButtons.CostSize.text = _upgradeInfo.SizeLvls[_sizeLvl].CostUpgrade.ToString() + "$";
        }
    }

    private void UpgradeFuel()
    {
        if (_fuelLvl != _upgradeInfo.FuelLvls.Count &&
            GameManager.Instance.SpendMoney(_upgradeInfo.FuelLvls[_fuelLvl].CostUpgrade))
        {
            _fuel.SetFuel(_upgradeInfo.FuelLvls[_fuelLvl].Volume);
            
            _fuelLvl++;
        }

        if (_fuelLvl == _upgradeInfo.FuelLvls.Count)
        {
            Debug.Log("MaxLvlFuel");
            _upgradeButtons.UpgradeFuel.interactable = false;

            _upgradeButtons.CostFuel.text = "Max Lvl";
        }
        else
        {
            _upgradeButtons.CostFuel.text = _upgradeInfo.FuelLvls[_fuelLvl].CostUpgrade.ToString() + "$";
        }
    }

    private void UpgradePower()
    {
        if (_powerLvl != _upgradeInfo.PowerLvls.Count &&
            GameManager.Instance.SpendMoney(_upgradeInfo.PowerLvls[_powerLvl].CostUpgrade))
        {
            _saw.LevelUp(_upgradeInfo.PowerLvls[_powerLvl].SpeedMotor, _upgradeInfo.PowerLvls[_powerLvl].Damage);
            _powerLvl++;
        }

        if (_powerLvl == _upgradeInfo.PowerLvls.Count)
        {
            Debug.Log("MaxLvlPower");
            _upgradeButtons.UpgradePower.interactable = false;

            _upgradeButtons.CostPower.text = "Max Lvl";
        }
        else
        {
            _upgradeButtons.CostPower.text = _upgradeInfo.PowerLvls[_powerLvl].CostUpgrade.ToString() + "$";
        }
    }

    private void UpgradeCrasher()
    {
        if (_lengthLvl != _upgradeInfo.LengthLvls.Count &&
            GameManager.Instance.SpendMoney(_upgradeInfo.LengthLvls[_lengthLvl].CostUpgrade))
        {
            SpawnCrasher();
        }

        if (_lengthLvl == _upgradeInfo.LengthLvls.Count)
        {
            Debug.Log("MaxLvlLength");
            _upgradeButtons.UpgradeLength.interactable = false;

            _upgradeButtons.CostLength.text = "Max Lvl";
        }
        else
        {
            _upgradeButtons.CostLength.text = _upgradeInfo.LengthLvls[_lengthLvl].CostUpgrade.ToString() + "$";
        }
    }

    private void SpawnCrasher()
    {
        if (_currentCrasher != null)
        {
            _saw.transform.parent = null;
            Destroy(_currentCrasher.gameObject);
        }

        _currentCrasher = Instantiate(_upgradeInfo.LengthLvls[_lengthLvl].Crasher, _crasherPos);
        _currentCrasher.transform.parent = transform;
        _currentCrasher.JoystickInfo = _joystick;

        _saw.transform.parent = _currentCrasher.SawPosition;
        _saw.transform.localPosition = Vector3.zero;

        _cinemachineVirtualCamera.Follow = _currentCrasher.SawPosition;

        _crasherRig = _currentCrasher.CrasherRig;

        _lengthLvl++;
    }
    
    private void OnApplicationQuit()
    {
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        UIManager.Instance.StartGame -= StartGame;
        UIManager.Instance.EndGame -= EndGame;
    }
}

public enum UpgradeType
{
    Power,
    Fuel,
    Size,
    Length
}