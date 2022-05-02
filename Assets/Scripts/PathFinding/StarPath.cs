using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class StarPath : MonoBehaviour, IPath
    {
        private readonly Vector3Int[] _offsets = {Vector3Int.up, Vector3Int.left, Vector3Int.down, Vector3Int.right};
        private Floor _floor;

        private void Awake()
        {
            _floor = FindObjectOfType<Floor>();
        }

        public Vector3Int? GetPath(Vector3Int from, Vector3Int to)
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
                    if (node.GetCost() < currentNode.GetCost())
                        currentNode = node;

                openList.Remove(currentNode);
                closedList.Add(currentNode.GetPosition());
                // get valid neighbours of current cell
                foreach (var offset in _offsets)
                {
                    var neighbour = currentNode.GetPosition() + offset;
                    // check if neighbour is the destination
                    if (neighbour == to)
                    {
                        var node = new StarNode(neighbour, currentNode, to);
                        while (node.GetParent().GetParent() != null)
                        {
                            // print(node.GetPosition()); // DEBUG
                            node = node.GetParent();
                        }
                        return node.GetPosition();
                    }

                    // skip if neighbour cell can't be walked on, or it has already been checked
                    if (!_floor.IsValidTile(neighbour) || closedList.Contains(neighbour))
                        continue;
                    openList.Add(new StarNode(neighbour, currentNode, to));
                }
            }

            return null;
        }

        public Vector3Int? RunAway(Vector3Int from, Vector3Int enemy)
        {
            throw new NotImplementedException();
        }
    }
}