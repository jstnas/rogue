using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Entities
{
    public class Zombie : Entity
    {
        [SerializeField] private int seed;
        private Decision _decisionMaker;


        protected override void Awake()
        {
            base.Awake();
            _decisionMaker = FindObjectOfType<Decision>();
            MovementEnded += EndTurn;
        }

        public override void OnTurn()
        {
            base.OnTurn();
            _decisionMaker.AIDecision(this);   
        }

        /*private bool Attack()
        {
            foreach (var offset in _offsets)
            {
                var position = GetCellPosition() + offset;
                var targetEntity = EntityManager.GetEntity(position);
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
        }*/
    }
}