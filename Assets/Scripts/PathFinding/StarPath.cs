using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public readonly struct StarNode
    {
        private readonly Vector3Int _position;
        private readonly StarNode? _parent;
        private readonly float _cost;

        public StarNode(Vector3Int position, StarNode? parent, Vector3Int to)
        {
            _position = position;
            _parent = parent;
            _cost = Vector3Int.Distance(position, to);
        }

        public Vector3Int GetPosition()
        {
            return _position;
        }

        public StarNode? GetParent()
        {
            return _parent;
        }

        public float GetCost()
        {
            return _cost;
        }
    }

    public class StarPath : Path
    {
        private readonly Vector3Int[] _offsets = {Vector3Int.up, Vector3Int.left, Vector3Int.down, Vector3Int.right};
        [SerializeField] private Floor floor;

        public override List<Vector3Int> GetPath(Vector3Int from, Vector3Int to)
        {
            // keeps track of cells that need to be visited
            var openList = new List<StarNode> {new StarNode(from, null, to)};
            // keeps track of cells that have already been visited
            var closedList = new List<Vector3Int>();
            while (openList.Count > 0)
            {
                var currentNode = openList[0];
                // get the closest cell in open list
                foreach (var node in openList)
                {
                    if (node.GetCost() < currentNode.GetCost())
                        currentNode = node;
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode.GetPosition());
                // get valid neighbours of current cell
                foreach (var offset in _offsets)
                {
                    var neighbour = currentNode.GetPosition() + offset;
                    // check if neighbour is the destination
                    if (neighbour == to)
                    {
                        var path = new List<Vector3Int>();
                        StarNode? node = new StarNode(neighbour, currentNode, to);
                        while (node != null)
                        {
                            path.Add(node.Value.GetPosition());
                            node = node.Value.GetParent();
                        }
                        return path;
                    }
                    // skip if neighbour cell can't be walked on, or it has already been checked
                    if (!floor.IsValidTile(neighbour) || closedList.Contains(neighbour))
                        continue;
                    openList.Add(new StarNode(neighbour, currentNode, to));
                }
            }

            return null;
        }
    }
}