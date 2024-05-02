using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [Header("Screens")] [SerializeField] private GameObject _upgradeScreen = null;
    [SerializeField] private GameObject _gameScreen = null;
    [SerializeField] private UIGameOver _gameOver = null;
    [SerializeField] private GameObject _winGame = null;

    [SerializeField] private TMP_Text _walletTxt = null;

    [Header("Buttons")] [SerializeField] private Button _back = null;
    [SerializeField] private Button _settings = null;
    [SerializeField] private Button _settingsClose = null;
    [SerializeField] private Button _play = null;

    [SerializeField] private Joystick _joystick = null;

    [SerializeField] private CrasherManager CCCM;
    [SerializeField] private CanvasGroup CCSETTINGS;
    [SerializeField] private AudioSource CCSounds;
    [SerializeField] private AudioSource CCMUSIC;
    
    [SerializeField] private Button CCSOUNDBTN;
    [SerializeField] private TMP_Text CCSOUNDTxt;
    [SerializeField] private Button CCMUSICBTN;
    [SerializeField] private TMP_Text CCMUSICTxt;

    [SerializeField] private AudioClip CCUpgrade;
    [SerializeField] private AudioClip CCTimeOut;
    [SerializeField] private AudioClip CCWin;
    [SerializeField] private AudioClip CCCoins;
    [SerializeField] private AudioClip CCDestroy;

    public System.Action StartGame;
    public System.Action EndGame;

    private Vector2 _screePixelSize;
    private Vector3 _defaultScreenPos;

    public void CCSFX(CCTYPE cctype)
    {
        switch (cctype)
        {
            case CCTYPE.TimeOut:
                CCSounds.PlayOneShot(CCTimeOut);
                break;
            case CCTYPE.Win:
                CCSounds.PlayOneShot(CCWin);
                break;
            case CCTYPE.Coin:
                CCSounds.PlayOneShot(CCCoins);
                break;
            case CCTYPE.Destroy:
                CCSounds.PlayOneShot(CCDestroy);
                break;
            case CCTYPE.Upgrade:
                CCSounds.PlayOneShot(CCUpgrade);
                break;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
        
        CloseSettings();

        Instance = this;

        _play.onClick.AddListener(PlayGame);
        _back.onClick.AddListener(Back);
        
        _settings.onClick.AddListener(OpenSettings);
        _settingsClose.onClick.AddListener(CloseSettings);

        _joystick.gameObject.SetActive(false);
        _gameScreen.SetActive(false);

        LoadSave();
    }

    private void CCCHANGEMusic()
    {
        CCMUSICTxt.text = "Music\n";

        var ccstatus = PlayerPrefs.GetString("CCMSC", "ON");

        if (ccstatus == "ON")
        {
            CCMUSICTxt.text += "<color=red>OFF</color>";
            CCMUSIC.mute = true;
            PlayerPrefs.SetString("CCMSC", "OFF");
        }
        else
        {
            CCMUSICTxt.text += "<color=green>ON</color>";
            CCMUSIC.mute = false;
            PlayerPrefs.SetString("CCMSC", "ON");
        }
    }
    
    private void CCCHANGESounds()
    {
        CCSOUNDTxt.text = "Sounds\n";
        
        var ccstatus = PlayerPrefs.GetString("CCSND", "ON");
        if (ccstatus == "ON")
        {
            CCSOUNDTxt.text += "<color=red>OFF</color>";
            CCSounds.mute = true;
            PlayerPrefs.SetString("CCSND", "OFF");
        }
        else
        {
            CCSOUNDTxt.text += "<color=green>ON</color>";
            CCSounds.mute = false;
            PlayerPrefs.SetString("CCSND", "ON");
        }
    }

    private void Start()
    {
        GameManager.Instance.MoneyTrans += UpdateWallet;
        LvlStatus.Instance.WinGame += Win;
        
        InitializedScreens();
        
        CCMUSICTxt.text = "Music\n";
        var ccstatus = PlayerPrefs.GetString("CCMSC", "ON");
        if (ccstatus == "OFF")
        {
            CCMUSICTxt.text += "<color=red>OFF</color>";
            CCMUSIC.mute = true;
        }
        else
        {
            CCMUSICTxt.text += "<color=green>ON</color>";
            CCMUSIC.mute = false;
        }
        
        CCSOUNDTxt.text = "Sounds\n";
        var ccsndstatus = PlayerPrefs.GetString("CCSND", "ON");
        if (ccsndstatus == "OFF")
        {
            CCSOUNDTxt.text += "<color=red>OFF</color>";
            CCSounds.mute = true;
        }
        else
        {
            CCSOUNDTxt.text += "<color=green>ON</color>";
            CCSounds.mute = false;
        }
        
        CCSOUNDBTN.onClick.AddListener(CCCHANGESounds);
        CCMUSICBTN.onClick.AddListener(CCCHANGEMusic);
    }

    private void InitializedScreens()
    {
        _screePixelSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        
        _defaultScreenPos = new Vector3(_screePixelSize.x * 1.5f, _screePixelSize.y / 2, 0);

        _gameOver.transform.position = _defaultScreenPos;
        _winGame.transform.position = _defaultScreenPos;
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

            _walletTxt.text = moneyf.ToString("#.#") + "k";
        }
        else
        {
            _walletTxt.text = money.ToString();
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
        CCSETTINGS.alpha = 1f;
        CCSETTINGS.interactable = true;
        CCSETTINGS.blocksRaycasts = true;
    }
    
    private void CloseSettings()
    {
        CCSETTINGS.alpha = 0f;
        CCSETTINGS.interactable = false;
        CCSETTINGS.blocksRaycasts = false;
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
        CCSFX(CCTYPE.TimeOut);
        _gameOver.GameOver(true, GameManager.Instance.MoneySession);
        
        MovingGameOverScreen();
    }

    private void Win()
    {
        _winGame.transform.DOMoveX(_screePixelSize.x / 2, 1f)
            .OnComplete(() =>
            {
                CCCM.CCRESETSave();
                SceneManager.LoadScene(0);
            });
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
                        _gameOver.transform.position = _defaultScreenPos);
            });
    }

    private void OnApplicationQuit()
    {
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        GameManager.Instance.MoneyTrans -= UpdateWallet;
        LvlStatus.Instance.WinGame -= Win;
    }
}

public enum CCTYPE
{
    TimeOut,
    Win,
    Coin,
    Destroy,
    Upgrade
}