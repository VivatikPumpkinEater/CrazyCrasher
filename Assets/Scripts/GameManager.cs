using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private int _wallet = 0;
    private int _moneySession = 0;

    public int MoneySession => _moneySession;
    public System.Action<int> MoneyTrans;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;
        
        LoadSaveMoney();
    }

    private void LoadSaveMoney()
    {
        if (PlayerPrefs.HasKey("Wallet"))
        {
            _wallet = PlayerPrefs.GetInt("Wallet");
        }
    }

    private void Start()
    {
        UIManager.Instance.EndGame += ResetMoneySession;
    }

    private void ResetMoneySession()
    {
        Debug.Log("Money = " + _moneySession);
        _moneySession = 0;
        
        SaveMoney();
    }

    public void AddMoney(int value)
    {
        _wallet += value;
        _moneySession += value;
        
        MoneyTrans?.Invoke(_wallet);
    }

    public bool SpendMoney(int cost)
    {
        if (_wallet - cost >= 0)
        {
            _wallet -= cost;
            
            MoneyTrans?.Invoke(_wallet);
            return true;
        }

        return false;
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("Wallet", _wallet);
    }
}
