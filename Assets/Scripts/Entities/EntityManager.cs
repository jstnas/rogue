using System.Collections.Generic;
using JetBrains.Annotations;
using PathFinding.Maps;
using Ui;
using UnityEngine;

namespace Entities
{
    public delegate void EntitiesUpdated(List<Entity> entities);

    public class EntityManager : MonoBehaviour
    {
        [SerializeField] private GameOverPanel gameOverPanel;
        [SerializeField] private Map[] maps;
        private readonly List<Entity> _entities = new List<Entity>();
        private int _currentEntity;
        private int _playerTurns; // number of turns the player has made
        public EntitiesUpdated EntitiesUpdated;

        private void Start()
        {
            // add all the entities
            var entities = GetComponentsInChildren<Entity>();
            foreach (var entity in entities)
            {
                _entities.Add(entity);
                if (entity.CompareTag("Player")) entity.onTurnEnded.AddListener(OnPlayerTurnEnded);
                entity.onTurnEnded.AddListener(OnTurnEnded);
                entity.onEntityDied.AddListener(OnEntityDeath);
            }

            EntitiesUpdated?.Invoke(_entities);

            // let the first entity take its turn
            _entities[0].OnTurn();
        }

        private void OnEntityDeath(Entity arg0)
        {
            _entities.Remove(arg0);
            EntitiesUpdated?.Invoke(_entities);
            // end game if player dies
            if (arg0.CompareTag("Player"))
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

        private void OnTurnEnded(Entity arg0)
        {
            _currentEntity++;
            _currentEntity %= _entities.Count;
            _entities[_currentEntity].OnTurn();
        }

        private void OnPlayerTurnEnded(Entity arg0)
        {
            _playerTurns++;
        }

        [CanBeNull]
        public Entity GetEntity(Vector3Int cellPosition)
        {
            foreach (var entity in _entities)
                if (entity.GetCellPosition() == cellPosition)
                    return entity;
            return null;
        }

        private void Stop()
        {
            // stop advancing turns
            foreach (var entity in _entities)
                entity.onTurnEnded.RemoveListener(OnTurnEnded);
        }
    }
}