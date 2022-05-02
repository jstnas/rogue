using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace PathFinding.Maps
{
    public class Map : MonoBehaviour
    {
        [SerializeField] protected List<Entity> targets;

        protected readonly Vector3Int[] NeighbourOffsets =
        {
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right,
            Vector3Int.up
        };

        protected readonly Dictionary<Vector3Int, int> Tiles = new Dictionary<Vector3Int, int>();
        protected Floor Floor;

        private void Awake()
        {
            Floor = GetComponent<Floor>();
        }

        public virtual void UpdateMap()
        {
            Tiles.Clear();
        }
    }
}