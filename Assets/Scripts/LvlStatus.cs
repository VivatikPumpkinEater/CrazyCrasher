using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlStatus : MonoBehaviour
{
    public static LvlStatus Instance = null;

    [SerializeField] private Image _lvlStatus = null;
    [SerializeField] private SpawnerVoxModels _spawner = null;

    private int _cellsCount = 0;

    private float _step = 0f;

    public System.Action WinGame;

    private bool _winGame = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;

        _cellsCount = _spawner.CurrentModel.CellsCount;

        _step = 1f / _cellsCount;

        _lvlStatus.fillAmount = 0f;
    }

    private void Start()
    {
        UIManager.Instance.EndGame += ResetLvlProgress;
    }

    public void ProgressLvl()
    {
        if(!_winGame)
        {
            _lvlStatus.fillAmount += _step;

            if (_lvlStatus.fillAmount >= 0.98f)
            {
                _winGame = true;
                
                UIManager.Instance.CCSFX(CCTYPE.Win);
                SpawnerVoxModels.Instance.CCCOMPLVL();
                WinGame?.Invoke();
            }
        }
    }

    private void ResetLvlProgress()
    {
        _lvlStatus.fillAmount = 0f;
        _winGame = false;
    }

    private void OnApplicationQuit()
    {
        UIManager.Instance.EndGame -= ResetLvlProgress;
    }
}