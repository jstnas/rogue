using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    private Tilemap _tilemap;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    public bool IsValidTile(Vector3Int tile)
    {
        return _tilemap.GetTile(tile) != null;
    }
}