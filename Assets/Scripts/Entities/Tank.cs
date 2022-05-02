using PathFinding;
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
            _path = FindObjectOfType<TankMap>();
            _target = GameObject.FindWithTag("Player").GetComponent<Entity>();
            MovementEnded += EndTurn;
        }

        public override void OnTurn()
        {
            base.OnTurn();
            var path = _path.GetPath(GetCellPosition(), _target.GetCellPosition());
            print(path);
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