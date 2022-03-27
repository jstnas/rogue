using UnityEngine;

public class Player : Entity
{
    // keeps track of when to check for input
    private bool _checkingInput;

    private void Update()
    {
        // skip if not checking input
        if (!_checkingInput)
            return;
        var newDirection = GetDirection();
        // skip if no key was pressed
        if (newDirection is null)
            return;
        var newPosition = Movement.GetOffsetPosition(newDirection.Value);
        // check if can attack entity at new position
        var targetEntity = Entities.GetEntity(newPosition);
        if (targetEntity != null)
        {
            Combat.Attack(targetEntity);
            _checkingInput = false;
            EndTurn();
            return;
        }

        // skip if can't move to new position
        if (!Floor.IsValidTile(newPosition))
            return;
        Movement.Move(newDirection.Value);
        _checkingInput = false;
    }

    public override void OnTurn()
    {
        _checkingInput = true;
        base.OnTurn();
    }

    private static Direction? GetDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
            return Direction.Up;
        if (Input.GetKeyDown(KeyCode.A))
            return Direction.Left;
        if (Input.GetKeyDown(KeyCode.S))
            return Direction.Down;
        if (Input.GetKeyDown(KeyCode.D))
            return Direction.Right;
        return null;
    }
}