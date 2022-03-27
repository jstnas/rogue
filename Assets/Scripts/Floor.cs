using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    private readonly List<Vector3Int> _floorTiles = new List<Vector3Int>();

    private void Start()
    {
        // get all floor tiles
        floorTilemap.CompressBounds();
        var bounds = floorTilemap.cellBounds;
        var tiles = floorTilemap.GetTilesBlock(bounds);
        for (var x = 0; x < bounds.size.x; x++)
        for (var y = 0; y < bounds.size.y; y++)
        {
            var tile = tiles[x + y * bounds.size.x];
            // skip empty tiles
            if (tile == null)
                continue;
            var cellPosition = new Vector3Int(
                x + bounds.position.x,
                y + bounds.position.y,
                0
            );
            _floorTiles.Add(cellPosition);
        }
    }

    public bool IsValidTile(Vector3Int cellPosition)
    {
        return _floorTiles.Contains(cellPosition);
    }
}