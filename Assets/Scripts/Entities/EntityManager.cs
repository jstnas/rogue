using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Ui;
using UnityEngine;

namespace Entities
{
    public delegate void EntitiesUpdated(List<Entity> entities);
    public class EntityManager : MonoBehaviour
    {
        public EntitiesUpdated EntitiesUpdated;
        [SerializeField] private GameOverPanel gameOverPanel;
        private readonly List<Entity> _entities = new List<Entity>();
        private int _currentEntity;
        private int _playerTurns; // number of turns the player has made

        private void Start()
        {
            // add all the entities
            var entities = GetComponentsInChildren<Entity>();
            foreach (var entity in entities)
            {
                _entities.Add(entity);
                entity.TurnEnded += OnTurnEnded;
                if (entity.CompareTag("Player"))
                    entity.TurnEnded += TrackTurn;
                entity.EntityDied += OnEntityDeath;
            }
            EntitiesUpdated?.Invoke(_entities);

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
            EntitiesUpdated?.Invoke(_entities);
            // end game if player dies
            if (entity.CompareTag("Player"))
            {
                gameOverPanel.Display(false);
                print("<color=red>Game over! player died!</color>");
                Stop();
            }
            // end game if only player is left
            else if (_entities.Count == 1)
            {
                gameOverPanel.SetTurns(_playerTurns);
                gameOverPanel.Display(true);
                print("<color=green>Game over! player won!</color>");
                Stop();
            }
        }

        private void OnTurnEnded()
        {
            _currentEntity++;
            _currentEntity %= _entities.Count;
            _entities[_currentEntity].OnTurn();
        }

        private void TrackTurn()
        {
            _playerTurns++;
        }

        private void Stop()
        {
            foreach (var entity in _entities)
            {
                entity.TurnEnded -= OnTurnEnded;
            }
        }
    }
}