using PathFinding;
using UnityEngine;

namespace Entities
{
    public class Knight : Entity
    {
        private Entity _target;
        private Path _path;
        protected override void Awake()
        {
            base.Awake();
            _path = GetComponent<Path>();
            _target = GameObject.FindWithTag("Player").GetComponent<Entity>();
            MovementEnded += EndTurn;
        }

        public override void OnTurn()
        {
            base.OnTurn();
            var path = _path.GetPath(GetCellPosition(), _target.GetCellPosition());
            var nextCell = path[0];
            // attack the player if next to them
            var targetEntity = EntityManager.GetEntity(nextCell);
            if (targetEntity != null)
            {
                if (targetEntity.CompareTag("Player"))
                {
                    Attack(targetEntity);
                    EndTurn();
                    return;
                }
                // skip turn if path is occupied by another entity
                else
                {
                    EndTurn();
                    return;
                }
            }
            // move otherwise
            MoveTo(nextCell);
        }
    }
}