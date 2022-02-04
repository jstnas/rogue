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

    public bool IsValidTile(Vector2Int cellPosition)
    {
        var tilePosition = new Vector3Int(cellPosition.x, cellPosition.y, 0);
        return _tilemap.GetTile(tilePosition) != null;
    }
}