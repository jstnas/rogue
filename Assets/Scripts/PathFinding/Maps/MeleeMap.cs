using System.Collections.Generic;
using UnityEngine;

namespace PathFinding.Maps
{
    public class MeleeMap : Map, IPath
    {
        public Vector3Int? GetPath(Vector3Int from, Vector3Int to)
        {
            // pick the lowest neighbour tile
            Vector3Int? targetTile = null;
            var targetWeight = int.MaxValue;
            // go through neighbour tiles
            foreach (var offset in NeighbourOffsets)
            {
                var neighbourTile = from + offset;
                // skip invalid tiles
                if (!Tiles.ContainsKey(neighbourTile))
                    continue;
                var neighbourWeight = Tiles[neighbourTile];
                // skip tiles that are more expensive
                if (neighbourWeight >= targetWeight)
                    continue;
                targetWeight = neighbourWeight;
                targetTile = neighbourTile;
            }

            return targetTile;
        }

        public override void UpdateMap()
        {
            base.UpdateMap();
            var value = 0;
            // populate starting tiles
            var openTiles = new List<Vector3Int>();
            foreach (var target in targets)
                openTiles.Add(target.GetCellPosition());
            // generate all tiles
            while (openTiles.Count > 0)
            {
                var newTiles = new List<Vector3Int>();
                foreach (var tile in openTiles)
                {
                    // add current tile to map
                    Tiles.Add(tile, value);
                    // add neighbouring tiles to new tiles
                    foreach (var offset in NeighbourOffsets)
                    {
                        var newTile = tile + offset;
                        // add tile if it's unique and valid
                        if (!Tiles.ContainsKey(newTile) && !newTiles.Contains(newTile) && Floor.IsValidTile(newTile))
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