using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        UIManager.Instance.CCSFX(CCTYPE.Coin);

        var coin = Instantiate(_coinsPrefab, _spawnCoinsPoint.position, Quaternion.identity);
        
        GameManager.Instance.AddMoney(Random.Range(1, 11));
        
        Destroy(coin, 2f);
    }
}
