using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Direction
{
    Up,
    Left,
    Down,
    Right,
}

public delegate void MoveDelegate();

public class Movement : MonoBehaviour
{
    public MoveDelegate Moved;
    protected Direction Direction = Direction.Up;
    private const float Speed = 16f;
    private static readonly Vector3Int[] Offsets = {
        Vector3Int.up,
        Vector3Int.left,
        Vector3Int.down,
        Vector3Int.right,
    };
    private Grid _grid;
    private Ground _ground;
    private Vector3Int _position;

    public Vector3Int GetCellPosition()
    {
        return _position;
    }

    private void Awake()
    {
        _grid = FindObjectOfType<Grid>();
        _ground = _grid.GetComponent<Ground>();
    }

    private void Start()
    {
        _position = _grid.WorldToCell(transform.position);
    }

    protected virtual void Update()
    {
        Draw();
    }

    public virtual void Move()
    {
        var offset = Offsets[Convert.ToInt32(Direction)];
        var newPosition = _position + offset;
        // skip if can't move to tile
        if (!_ground.IsValidTile(newPosition))
            return;
        _position = newPosition;
        Moved?.Invoke();
    }

    private void Draw()
    {
        var targetPosition = _grid.CellToWorld(_position) + _grid.cellSize / 2;
        var t = transform;
        t.position = Vector3.Lerp(t.position, targetPosition, Speed * Time.deltaTime);
    }
}