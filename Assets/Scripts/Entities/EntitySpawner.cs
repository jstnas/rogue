using System;
using UnityEngine;

namespace Entities
{
    public class EntitySpawner : MonoBehaviour
    {
        [SerializeField] private Grid grid;
        [SerializeField] private EntityList entityList;

        private void Awake()
        {
            foreach (var entity in entityList.GetEntities())
            {
                var cellSize = grid.cellSize;
                var spawnPosition = new Vector3(
                    entity.position.x * cellSize.x + cellSize.x * 0.5f,
                    entity.position.y * cellSize.y + cellSize.y * 0.5f,
                    0);
                Instantiate(entity.prefab, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
}
