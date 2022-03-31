using JetBrains.Annotations;
using UnityEngine;

namespace PathFinding
{
    public class StarNode
    {
        private readonly float _cost;
        [CanBeNull] private readonly StarNode _parent;
        private readonly Vector3Int _position;

        public StarNode(Vector3Int position, StarNode parent, Vector3Int to)
        {
            _position = position;
            _parent = parent;
            _cost = Vector3Int.Distance(position, to);
        }

        public Vector3Int GetPosition()
        {
            return _position;
        }

        public StarNode GetParent()
        {
            return _parent;
        }

        public float GetCost()
        {
            return _cost;
        }
    }
}