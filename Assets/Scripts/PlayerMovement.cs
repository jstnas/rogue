using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Floor floor;
    private Vector2Int _position;
    private Vector2 _cellSize,
        _halfCell;

    private void Awake()
    {
        _cellSize = grid.cellSize;
        _halfCell = _cellSize * 0.5f;
    }

    private void Update()
    {
        Move();
        Draw();
    }

    private void Move()
    {
        // Get input.
        var input = Vector2Int.zero;
        if (Input.GetKeyDown(KeyCode.W))
            input = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.A))
            input = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.S))
            input = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.D))
            input = Vector2Int.right;
        // Only move on input.
        if (input == Vector2Int.zero)
            return;
        var newPosition = _position + input;
        // Skip if can't move to new position.
        if (!floor.IsValidTile(newPosition))
            return;
        _position = newPosition;
    }

    private void Draw()
    {
        var t = transform;
        var targetPosition = new Vector3(_position.x * _cellSize.x + _halfCell.x, _position.y * _cellSize.y + _halfCell.y, 0);
        t.position = Vector3.Lerp(t.position, targetPosition, 0.1f);
    }
}