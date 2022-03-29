using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Zombie : Entity
{
    [SerializeField] private int seed;
    private Floor _floor;

    private Vector3Int[] _offsets =
    {
        Vector3Int.up,
        Vector3Int.left,
        Vector3Int.down,
        Vector3Int.right
    };

    private Random _random;

    protected override void Awake()
    {
        base.Awake();
        _random = new Random(seed);
        _floor = FindObjectOfType<Floor>();
        MovementEnded += EndTurn;
    }

    public override void OnTurn()
    {
        base.OnTurn();
        // zombie will try to move in every direction before giving up
        ShuffleOffsets();
        if (Attack())
            return;
        if (Move())
            return;
        // do nothing
        EndTurn();
    }

    private bool Attack()
    {
        foreach (var offset in _offsets)
        {
            var position = GetCellPosition() + offset;
            var targetEntity = Entities.GetEntity(position);
            // only target player
            if (targetEntity == null ||
                !targetEntity.CompareTag("Player"))
                continue;
            Attack(targetEntity);
            EndTurn();
            return true;
        }
        return false;
    }

    private bool Move()
    {
        foreach (var offset in _offsets)
        {
            // move in a random direction
            var position = GetCellPosition() + offset;
            // try next direction if can't move in new direction
            if (!_floor.IsValidTile(position))
                continue;
            MoveTo(position);
            return true;
        }
        return false;
    }

    private void ShuffleOffsets()
    {
        _offsets = _offsets.OrderBy(x => _random.Next()).ToArray();
    }
}