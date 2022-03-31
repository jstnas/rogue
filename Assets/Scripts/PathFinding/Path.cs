using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class Path : MonoBehaviour
    {
        public virtual List<Vector3Int> GetPath(Vector3Int from, Vector3Int to)
        {
            throw new NotImplementedException();
        }
    }
}