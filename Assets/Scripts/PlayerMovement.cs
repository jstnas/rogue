using UnityEngine;

public class PlayerMovement : Movement
{
    protected override void Update()
    {
        Move();
        base.Update();
    }

    protected override void Move()
    {
        // Get input.
        if (Input.GetKeyDown(KeyCode.W))
            direction = Direction.Up;
        else if (Input.GetKeyDown(KeyCode.A))
            direction = Direction.Left;
        else if (Input.GetKeyDown(KeyCode.S))
            direction = Direction.Down;
        else if (Input.GetKeyDown(KeyCode.D))
            direction = Direction.Right;
        else
            return;
        base.Move();
    }
}