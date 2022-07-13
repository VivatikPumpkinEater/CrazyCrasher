using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [Header("Screens")] [SerializeField] private GameObject _upgradeScreen = null;
    [SerializeField] private GameObject _gameScreen = null;
    [SerializeField] private UIGameOver _gameOver = null;

    [SerializeField] private TMP_Text _walletTxt = null;

    [Header("Buttons")] [SerializeField] private Button _back = null;
    [SerializeField] private Button _settings = null;
    [SerializeField] private Button _play = null;

    [SerializeField] private Joystick _joystick = null;

    public System.Action StartGame;
    public System.Action EndGame;

    private Vector2 _screePixelSize;

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

        _screePixelSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        _gameOver.transform.position =  new Vector3(_screePixelSize.x * 1.5f, _screePixelSize.y / 2, 0);

        LoadSave();
    }

    private void Start()
    {
        GameManager.Instance.MoneyTrans += UpdateWallet;
    }

    private void LoadSave()
    {
        if (PlayerPrefs.HasKey("Wallet"))
        {
            UpdateWallet(PlayerPrefs.GetInt("Wallet"));
        }
        else
        {
            UpdateWallet(0);
        }
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
        _gameOver.GameOver(false, GameManager.Instance.MoneySession);
        

        MovingGameOverScreen();
    }

    private void ResetGameElement()
    {
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

    public void GameOver()
    {
        _gameOver.GameOver(true, GameManager.Instance.MoneySession);
        
        
        MovingGameOverScreen();
    }

    private void MovingGameOverScreen()
    {
        _gameOver.transform.DOMoveX(_screePixelSize.x / 2, 1f)
            .OnComplete(() =>
            {
                EndGame?.Invoke();
                
                ResetGameElement();
                _gameOver.transform.DOMoveX(_screePixelSize.x / 2 * -1, 1f)
                    .OnComplete(() =>
                        _gameOver.transform.position = new Vector3(_screePixelSize.x * 1.5f, _screePixelSize.y / 2, 0));
            });
    }
}