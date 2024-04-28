using System.Collections.Generic;
using UnityEngine;

public class VoxSettings : MonoBehaviour
{
    [SerializeField] public List<Transform> _micros = new List<Transform>();
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
    public void RandomizeRotation()
    {
        foreach (var micro in _micros)
        {
            micro.rotation = Quaternion.Euler(Random.Range(-15, 16), Random.Range(-15, 16), 0);
        }
    }
}