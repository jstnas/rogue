using System.Collections.Generic;
using UnityEngine;

public delegate void EnemiesUpdated();

public class Enemies : MonoBehaviour
{
    public EnemiesUpdated EnemiesUpdated;
    [SerializeField] private Movement player;
    private int _movingEnemy;
    private readonly List<Movement> _enemies = new List<Movement>();

    public List<Movement> GetEnemies()
    {
        return _enemies;
    }

    private void Start()
    {
        player.Moved += OnPlayerMove;
        // add all the enemies
        var enemies = GetComponentsInChildren<Movement>();
        foreach (var enemy in enemies)
        {
            _enemies.Add(enemy);
        }
        EnemiesUpdated?.Invoke();
    }

    private void OnPlayerMove()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Move();
        }
    }
}