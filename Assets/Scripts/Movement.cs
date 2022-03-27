using System;
using UnityEngine;

public enum Direction
{
    Up,
    Left,
    Down,
    Right
}

public delegate void FinishedMovement();

public sealed class Movement : MonoBehaviour
{
    private const float DistanceThreshold = 0.1f;
    private const float Speed = 16f;

    private static readonly Vector3Int[] Offsets =
    {
        Vector3Int.up,
        Vector3Int.left,
        Vector3Int.down,
        Vector3Int.right
    };

    private Floor _floor;
    private Grid _grid;
    private bool _moving;
    private Vector3Int _position;

    private void Awake()
    {
        _grid = FindObjectOfType<Grid>();
        _floor = _grid.GetComponent<Floor>();
    }

    private void Start()
    {
        _position = _grid.WorldToCell(transform.position);
    }

    private void Update()
    {
        // skip if not moving
        if (!_moving)
            return;
        // calculate new position
        var targetPosition = _grid.CellToWorld(_position) + _grid.cellSize / 2;
        var position = transform.position;
        position = Vector3.Lerp(position, targetPosition, Speed * Time.deltaTime);
        // check if close enough to the target position
        var distance = Vector3.Distance(position, targetPosition);
        if (distance <= DistanceThreshold)
        {
            position = targetPosition;
            _moving = false;
            FinishedMovement?.Invoke();
        }

        // update position
        transform.position = position;
    }

    public event FinishedMovement FinishedMovement;

    public Vector3Int GetCellPosition()
    {
        return _position;
    }

    public void Move(Direction direction)
    {
        var newPosition = GetOffsetPosition(direction);
        // skip if can't move to tile
        if (!_floor.IsValidTile(newPosition))
        {
            FinishedMovement?.Invoke();
            return;
        }

        _position = newPosition;
        _moving = true;
    }

    public Vector3Int GetOffsetPosition(Direction direction)
    {
        return _position + Offsets[Convert.ToInt32(direction)];
    }
}