using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;
    
    [Header("Screens")]
    [SerializeField] private GameObject _upgradeScreen = null;
    [SerializeField] private GameObject _gameScreen = null;

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
