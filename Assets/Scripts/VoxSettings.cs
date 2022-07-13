using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxSettings : MonoBehaviour
{
    [SerializeField] private List<Transform> _micros = new List<Transform>();
    [SerializeField] private List<Material> _materials = new List<Material>();

    public int CellsCount => _micros.Count;

    [ContextMenu("ResetRotation")]
    private void ResetRotation()
    {
        foreach (var micro in _micros)
        {
            micro.rotation = Quaternion.identity;
        }
    }

    [ContextMenu("RandomizeRotation")]
    private void RandomizeRotation()
    {
        foreach (var micro in _micros)
        {
            micro.rotation = Quaternion.Euler(Random.Range(-15, 16), Random.Range(-15, 16), 0);
            
            if (_materials.Count != 0)
            {
                micro.GetComponent<MeshRenderer>().material = _materials[Random.Range(0, _materials.Count)];
            }
        }
    }
}