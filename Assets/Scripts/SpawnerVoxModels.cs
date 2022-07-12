using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerVoxModels : MonoBehaviour
{
    public static SpawnerVoxModels Instance = null;
    
    [SerializeField] private VoxSettings _currentModel = null;
    [SerializeField] private VoxSettings _prefabModel = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;
    }

    public void Respawn()
    {
        Destroy(_currentModel.gameObject);

        _currentModel = Instantiate(_prefabModel, transform);
    }
}
