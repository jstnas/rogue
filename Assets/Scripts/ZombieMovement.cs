using UnityEngine;

public class ZombieMovement : Movement
{
    public override void Move()
    {
        Direction = (Direction)Random.Range(0, 4);
        base.Move();
    }
}