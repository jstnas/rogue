using PathFinding;
using UnityEngine;

namespace Entities
{
    public class Knight : Entity
    {
        [SerializeField] private Entity target;
        private Path _path;
        protected override void Awake()
        {
            base.Awake();
            _path = GetComponent<Path>();
            MovementEnded += EndTurn;
        }

        public override void OnTurn()
        {
            base.OnTurn();
            var path = _path.GetPath(GetCellPosition(), target.GetCellPosition());
            var nextCell = path[0];
            // attack the player if next to them
            var targetEntity = EntityManager.GetEntity(nextCell);
            if (targetEntity != null && targetEntity.CompareTag("Player"))
            {
                Attack(targetEntity);
                EndTurn();
            }
            // move otherwise
            MoveTo(nextCell);
        }
    }
}