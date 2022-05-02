using UnityEngine;

namespace PathFinding
{
    public interface IPath
    {
        public Vector3Int? GetPath(Vector3Int from, Vector3Int to);
    }
}