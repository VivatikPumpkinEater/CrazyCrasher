using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;
    
    [Header("Screens")]
    [SerializeField] private GameObject _upgradeScreen = null;
    [SerializeField] private GameObject _gameScreen = null;

    [SerializeField] private TMP_Text _walletTxt = null;

    [Header("Buttons")] [SerializeField] private Button _back = null;
    [SerializeField] private Button _settings = null;
    [SerializeField] private Button _play = null;

    [SerializeField] private Joystick _joystick = null;
    
    public System.Action StartGame;
    public System.Action EndGame;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;
        
        _play.onClick.AddListener(PlayGame);
        _back.onClick.AddListener(Back);
        
        _joystick.gameObject.SetActive(false);
        _gameScreen.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.MoneyTrans += UpdateWallet;
    }

    private void UpdateWallet(int money)
    {
        if (money >= 1000)
        {
            float moneyf = money / 1000f;

            _walletTxt.text = moneyf.ToString("#.#") + "k$";
        }
        else
        {
            _walletTxt.text = money.ToString() + "$";
        }
    }

    private void Back()
    {
        EndGame?.Invoke();
        
        SpawnerVoxModels.Instance.Respawn();
        
        _joystick.gameObject.SetActive(false);
        _gameScreen.SetActive(false);
        _upgradeScreen.SetActive(true);
    }

    private void OpenSettings()
    {
        
    }

    private void PlayGame()
    {
        StartGame?.Invoke();
        
        _joystick.gameObject.SetActive(true);
        _gameScreen.SetActive(true);
        _upgradeScreen.SetActive(false);
    }
}
