using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Entities : MonoBehaviour
{
    private int _currentEntity;
    private readonly List<Entity> _entities = new List<Entity>();

    [CanBeNull]
    public Entity GetEntity(Vector3Int cellPosition)
    {
        foreach (var entity in _entities)
        {
            if (entity.GetComponent<Movement>().GetCellPosition() == cellPosition)
                return entity;
        }
        return null;
    }

    private void Start()
    {
        // add all the entities
        var entities = GetComponentsInChildren<Entity>();
        foreach (var entity in entities)
        {
            _entities.Add(entity);
            entity.OnTurnEnded += OnEntityTurnEnded;
        }
        // let the first entity take its turn
        _entities[0].OnTurn();
    }

    private void OnEntityTurnEnded()
    {
        _currentEntity++;
        _currentEntity %= _entities.Count;
        _entities[_currentEntity].OnTurn();
    }
}