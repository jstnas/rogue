using PathFinding;
using PathFinding.Maps;
using UnityEngine;

namespace Entities
{
    public class Tank : Entity
    {
        
        private Entity _target;
        private IPath _path;
        protected override void Awake()
        {
            base.Awake();
            _path = GetComponent<IPath>();
            _target = GameObject.FindWithTag("Player").GetComponent<Entity>();
            onMovementEnded.AddListener(EndTurn);
        }

        public override void OnTurn()
        {
            base.OnTurn();
            Vector3Int? path = null;
            // move towards healer if on low health
            if (NeedsHealing())
            {
                var closestHealer = EntityManager.GetClosestEntity(GetCellPosition(), "Healer");
                if (closestHealer != null)
                    path = _path.GetPath(GetCellPosition(), closestHealer.GetCellPosition());
            }
            // move towards target otherwise
            path ??= _path.GetPath(GetCellPosition(), _target.GetCellPosition());
            // don't move if not path found
            if (path == null)
            {
                EndTurn();
                return;
            }
            var nextCell = path.Value;
            // check if entity in the way
            var targetEntity = EntityManager.GetEntity(nextCell);
            if (targetEntity)
            {
                // attack players
                if (targetEntity.CompareTag("Player"))
                    Attack(targetEntity);
                EndTurn();
                return;
            }
            // move otherwise
            MoveTo(nextCell);
        }
    }
}