using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class SpawnerEntry
    {
        public GameObject prefab;
        public Vector3Int position;
    }
}