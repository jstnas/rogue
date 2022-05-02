using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class TankMap : MonoBehaviour, IPath
    {
        private readonly Vector3Int[] _neighbourOffsets =
        {
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right,
            Vector3Int.up
        };

        private readonly Dictionary<Vector3Int, int> _tiles = new Dictionary<Vector3Int, int>();
        private Floor _floor;

        private void Awake()
        {
            _floor = GetComponent<Floor>();
        }

        public Vector3Int? GetPath(Vector3Int from, Vector3Int to)
        {
            // pick the lowest neighbour tile
            Vector3Int? targetTile = null;
            var targetWeight = int.MaxValue;
            // go through neighbour tiles
            foreach (var offset in _neighbourOffsets)
            {
                var neighbourTile = from + offset;
                // skip invalid tiles
                if (!_tiles.ContainsKey(neighbourTile))
                    continue;
                var neighbourWeight = (int) _tiles[neighbourTile];
                // skip tiles that are more expensive
                if (neighbourWeight >= targetWeight)
                    continue;
                targetWeight = neighbourWeight;
                targetTile = neighbourTile;
            }

            return targetTile;
        }

        public void UpdateTiles(List<Vector3Int> from)
        {
            _tiles.Clear();
            var value = 0;
            var openTiles = from;
            while (openTiles.Count > 0)
            {
                var newTiles = new List<Vector3Int>();
                foreach (var tile in openTiles)
                {
                    // add current tile to map
                    _tiles.Add(tile, value);
                    // add neighbouring tiles to new tiles
                    foreach (var offset in _neighbourOffsets)
                    {
                        var newTile = tile + offset;
                        // add tile if it's unique and valid
                        if (!_tiles.ContainsKey(newTile) && !newTiles.Contains(newTile) && _floor.IsValidTile(newTile))
                            newTiles.Add(newTile);
                    }
                }

                // next generation
                openTiles = newTiles;
                value++;
            }
        }
    }
}