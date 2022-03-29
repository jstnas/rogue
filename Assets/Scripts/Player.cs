using UnityEngine;

public class Player : Entity
{
    // keeps track of when to check for input
    private bool _checkingInput;
    private Floor _floor;

    protected override void Awake()
    {
        base.Awake();
        _floor = FindObjectOfType<Floor>();
        MovementEnded += EndTurn;
    }

    protected override void Update()
    {
        base.Update();
        // skip if not checking input
        if (!_checkingInput)
            return;
        var newOffset = GetOffset();
        // skip if no key was pressed
        if (newOffset is null)
            return;
        var newPosition = GetCellPosition() + newOffset.Value;
        // check if can attack entity at new position
        var targetEntity = Entities.GetEntity(newPosition);
        if (targetEntity != null)
        {
            Attack(targetEntity);
            _checkingInput = false;
            EndTurn();
            return;
        }
        // skip if can't move to new position
        if (!_floor.IsValidTile(newPosition))
            return;
        MoveTo(newPosition);
        _checkingInput = false;
    }

    public override void OnTurn()
    {
        base.OnTurn();
        _checkingInput = true;
    }

    private static Vector3Int? GetOffset()
    {
        if (Input.GetKeyDown(KeyCode.W))
            return Vector3Int.up;
        if (Input.GetKeyDown(KeyCode.A))
            return Vector3Int.left;
        if (Input.GetKeyDown(KeyCode.S))
            return Vector3Int.down;
        if (Input.GetKeyDown(KeyCode.D))
            return Vector3Int.right;
        return null;
    }
}