using System.Collections.Generic;
using UnityEngine;

public class SpawnerVoxModels : MonoBehaviour
{
    public static SpawnerVoxModels Instance = null;
    
    [SerializeField] private VoxSettings _currentModel;
    
    [SerializeField] private List<VoxSettings> _models;

    public VoxSettings CurrentModel => _currentModel;

    private int cccurlvl;
    private int cccurlvlFact;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;

        cccurlvl = PlayerPrefs.GetInt("CCLVL", 0);
        cccurlvlFact = PlayerPrefs.GetInt("CCLVLFACT", 0);
        
        _currentModel = Instantiate(CCGETPREFAB(), transform);
    }

    public void Respawn()
    {
        Destroy(_currentModel.gameObject);

        _currentModel = Instantiate(CCGETPREFAB(), transform);
    }

    private VoxSettings CCGETPREFAB()
    {
        return _models[cccurlvlFact];
    }

    public void CCCOMPLVL()
    {
        cccurlvl++;
        
        if (cccurlvl >= _models.Count)
        {
            var rand = Random.Range(0, _models.Count);
            while (rand == cccurlvlFact)
            {
                rand = Random.Range(0, _models.Count);
            }
            
            cccurlvlFact = rand;
        }
        else
        {
            cccurlvlFact = cccurlvl;
        }
        
        PlayerPrefs.SetInt("CCLVL", cccurlvl);
        PlayerPrefs.SetInt("CCLVLFACT", cccurlvlFact);
    }
}
