using UnityEngine;

public class PlayerMovement : Movement
{
    protected override void Update()
    {
        base.Update();
        // only move on keypress
        var newDirection = GetDirection();
        if (newDirection == null)
            return;
        Direction = (Direction) newDirection;
        Move();
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