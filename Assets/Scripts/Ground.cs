using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    private Enemies _enemies;
    private readonly List<Vector3Int> _validTiles = new List<Vector3Int>();
    private List<Movement> _enemyList;
    private PlayerMovement _player;

    private void Awake()
    {
        _enemies = FindObjectOfType<Enemies>();
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        _enemies.EnemiesUpdated += OnEnemiesUpdated;
        // get all valid tiles
        floorTilemap.CompressBounds();
        var bounds = floorTilemap.cellBounds;
        var tiles = floorTilemap.GetTilesBlock(bounds);
        for (var x = 0; x < bounds.size.x; x++)
        {
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
                _validTiles.Add(cellPosition);
            }
        }
    }

    private void OnEnemiesUpdated()
    {
        _enemyList = _enemies.GetEnemies();
    }

    public bool IsValidTile(Vector3Int cellPosition)
    {
        // check if the cell is a floor
        if (!_validTiles.Contains(cellPosition))
            return false;
        // check if player is standing on cell
        if (_player.GetCellPosition() == cellPosition)
            return false;
        // check if an enemy is standing on cell
        foreach (var enemy in _enemyList)
        {
            if (enemy.GetCellPosition() == cellPosition)
                return false;
        }
        return true;
    }
}