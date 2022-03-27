using System;
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
        Move(directions);
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

    private void Move(IEnumerable<int> directions)
    {
        foreach (var d in directions)
        {
            // move in a random direction
            var direction = (Direction) d;
            var position = Movement.GetOffsetPosition(direction);
            // try next direction if can't move in new direction
            if (!Floor.IsValidTile(position))
                continue;
            Movement.Move(direction);
            return;
        }
    }

    private static List<int> GetRandomDirections()
    {
        // initialise
        int[] directions = {0, 1, 2, 3};
        var random = new Random();
        return directions.OrderBy(x => random.Next()).ToList();
    }
}