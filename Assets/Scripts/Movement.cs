using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Grid grid;
    [SerializeField] private Floor floor;
    protected Direction direction;
    private Vector2Int _position;

    protected virtual void Update()
    {
        Draw();
    }

    protected virtual void Move()
    {
        var offset = direction switch
        {
            Direction.Up => Vector2Int.up,
            Direction.Down => Vector2Int.down,
            Direction.Left => Vector2Int.left,
            Direction.Right => Vector2Int.right,
            _ => throw new ArgumentOutOfRangeException()
        };
        var newPosition = _position + offset;
        if (!floor.IsValidTile(newPosition))
            return;
        _position = newPosition;
    }

    private void Draw()
    {
        var t = transform;
        var cellSize = grid.cellSize;
        var halfCell = cellSize / 2;
        var targetPosition = new Vector3(_position.x * cellSize.x + halfCell.x, _position.y * cellSize.y + halfCell.y, 0);
        t.position = Vector3.Lerp(t.position, targetPosition, speed * Time.deltaTime);
    }
}