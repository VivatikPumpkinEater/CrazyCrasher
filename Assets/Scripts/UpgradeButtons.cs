using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtons : MonoBehaviour
{
    [SerializeField] private Button _upgradeLength = null;
    [SerializeField] private TMP_Text _costLength = null;
    
    [SerializeField] private Button _upgradeFuel = null;
    [SerializeField] private TMP_Text _costFuel = null;
    
    [SerializeField] private Button _upgradePower = null;
    [SerializeField] private TMP_Text _costPower = null;
    
    [SerializeField] private Button _upgradeSize = null;
    [SerializeField] private TMP_Text _costSize = null;

    public Button UpgradeLength => _upgradeLength;
    public TMP_Text CostLength => _costLength;

    public Button UpgradeFuel => _upgradeFuel;
    public TMP_Text CostFuel => _costFuel;

    public Button UpgradePower => _upgradePower;
    public TMP_Text CostPower => _costPower;

    public Button UpgradeSize => _upgradeSize;
    public TMP_Text CostSize => _costSize;
}