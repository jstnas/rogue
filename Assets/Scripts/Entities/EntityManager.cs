using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Entities
{
    public class EntityManager : MonoBehaviour
    {
        private readonly List<Entity> _entities = new List<Entity>();
        private int _currentEntity;

        private void Start()
        {
            // add all the entities
            var entities = GetComponentsInChildren<Entity>();
            foreach (var entity in entities)
            {
                _entities.Add(entity);
                entity.TurnEnded += OnTurnEnded;
                entity.EntityDied += OnEntityDeath;
            }

            // let the first entity take its turn
            _entities[0].OnTurn();
        }

        [CanBeNull]
        public Entity GetEntity(Vector3Int cellPosition)
        {
            foreach (var entity in _entities)
                if (entity.GetCellPosition() == cellPosition)
                    return entity;
            return null;
        }

        private void OnEntityDeath(Entity entity)
        {
            _entities.Remove(entity);
        }

        private void OnTurnEnded()
        {
            _currentEntity++;
            _currentEntity %= _entities.Count;
            _entities[_currentEntity].OnTurn();
        }
    }
}