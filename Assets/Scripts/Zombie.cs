using System.Collections.Generic;
using System.Linq;

public class Zombie : Entity
{
    public override void OnTurn()
    {
        base.OnTurn();
        // zombie will try to move in every direction before giving up
        var directions = GetRandomDirections();
        Attack(directions);
        foreach (var direction in directions)
        {
            // move in a random direction
            var newDirection = (Direction) direction;
            var newPosition = Movement.GetOffsetPosition(newDirection);
            // try to attack entity at new position
            var targetEntity = Entities.GetEntity(newPosition);
            if (targetEntity != null)
            {
                print($"{name} is attacking {targetEntity.name}");
                EndTurn();
                return;
            }
            // try next direction if can't move in new direction
            if (!Floor.IsValidTile(newPosition))
                continue;
            Movement.Move(newDirection);
            return;
        }
        // do nothing
        EndTurn();
    }

    private void Attack(IEnumerable<int> directions)
    {
        foreach (var d in directions)
        {
            var direction = (Direction) d;
            var position = Movement.GetOffsetPosition(direction);
            var targetEntity = Entities.GetEntity(position);
            // only target player
            if (targetEntity == null ||
                !targetEntity.CompareTag("Player"))
                continue;
            Combat.Attack(targetEntity);
            return;
        }
    }

    private static List<int> GetRandomDirections()
    {
        // initialise
        int[] directions = {0, 1, 2, 3};
        var random = new System.Random();
        return directions.OrderBy(x => random.Next()).ToList();
    }
}