using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converter : MonoBehaviour
{
    [SerializeField] private Transform _spawnCoinsPoint = null;

    [SerializeField] private GameObject _coinsPrefab = null;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Cells"))
        {
            Destroy(col.gameObject);
            
            SpawnCoins();
        }
    }

    private void SpawnCoins()
    {
        var coin = Instantiate(_coinsPrefab, _spawnCoinsPoint.position, Quaternion.identity);
        
        Destroy(coin, 2f);
    }
}
