using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private int _wallet = 0;
    private int _moneySesion = 0;

    public System.Action<int> MoneyTrans;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.EndGame += ResetMoneySession;
    }

    private void ResetMoneySession()
    {
        Debug.Log("Money = " + _moneySesion);
        _moneySesion = 0;
    }

    public void AddMoney(int value)
    {
        _wallet += value;
        _moneySesion += value;
        
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
}
