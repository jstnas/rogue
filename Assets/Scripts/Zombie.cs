using UnityEngine;

public class Zombie : Entity
{
    public override void OnTurn()
    {
        base.OnTurn();
        // move in a random direction
        var newDirection = (Direction) Random.Range(0, 4);
        var newPosition = Movement.GetOffsetPosition(newDirection);
        // try to attack entity at new position
        var targetEntity = Entities.GetEntity(newPosition);
        if (targetEntity != null)
        {
            print($"{name} is attacking {targetEntity.name}");
            EndTurn();
            return;
        }
        // try to move in new direction
        if (Floor.IsValidTile(newPosition))
        {
           Movement.Move(newDirection);
           return;
        }
        // do nothing
        EndTurn();
    }
}