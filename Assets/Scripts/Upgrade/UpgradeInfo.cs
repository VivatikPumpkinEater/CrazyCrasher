using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeLvlsAndCost", menuName = "UpgradeInfo")]
public class UpgradeInfo : ScriptableObject
{
    [field: SerializeField] public List<LengthLvl> LengthLvls = new List<LengthLvl>();
    [field: SerializeField] public List<FuelLvl> FuelLvls = new List<FuelLvl>();
    [field: SerializeField] public List<PowerLvl> PowerLvls = new List<PowerLvl>();
    [field: SerializeField] public List<SizeLvl> SizeLvls = new List<SizeLvl>();
}

[System.Serializable]
public struct LengthLvl
{
    public CrasherController Crasher;
    public int CostUpgrade;
}
[System.Serializable]
public struct FuelLvl
{
    public float Volume;
    public int CostUpgrade;
}
[System.Serializable]
public struct PowerLvl
{
    public int SpeedMotor;
    public int Damage;
    public int CostUpgrade;
}
[System.Serializable]
public struct SizeLvl
{
    public Vector3 StepSize;
    public int CostUpgrade;
}
