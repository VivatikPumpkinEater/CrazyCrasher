using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtons : MonoBehaviour
{
    [SerializeField] private Button _upgradeLength = null;
    [SerializeField] private Button _upgradeFuel = null;
    [SerializeField] private Button _upgradePower = null;
    [SerializeField] private Button _upgradeSize = null;

    public Button UpgradeLength => _upgradeLength;

    public Button UpgradeFuel => _upgradeFuel;

    public Button UpgradePower => _upgradePower;

    public Button UpgradeSize => _upgradeSize;
}