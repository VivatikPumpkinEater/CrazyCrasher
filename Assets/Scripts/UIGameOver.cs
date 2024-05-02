using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneySession = null;

    [SerializeField] private GameObject _fuelOut = null;

    public void GameOver(bool gameOver, int moneySession)
    {
        _fuelOut.SetActive(gameOver);

        _moneySession.text ="+" + moneySession.ToString();
    }
}
