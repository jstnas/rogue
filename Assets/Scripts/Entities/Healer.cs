using PathFinding;
using PathFinding.Maps;
using UnityEngine;

namespace Entities
{
    public class Healer : Entity
    {
        private MeleeMap _meleeMap;
        private IPath _path;

        protected override void Awake()
        {
            base.Awake();
            _path = GetComponent<IPath>();
            _meleeMap = FindObjectOfType<MeleeMap>();
            onMovementEnded.AddListener(EndTurn);
        }

        public override void OnTurn()
        {
            base.OnTurn();
            // TODO: go through entities
            // priorities healing nearby entities
            // otherwise move towards them, and away from the player
            Entity? healingTarget = null;
            var targetDistance = float.PositiveInfinity;
            var entities = EntityManager.GetEntities();
            foreach (var entity in entities)
            {
                // skip entities that don't need healing, or players
                if (!ShouldHeal(entity))
                    continue;
                var distance = Vector3Int.Distance(GetCellPosition(), entity.GetCellPosition());
                // skip entities that are too far away
                if (distance >= targetDistance)
                    continue;
                healingTarget = entity;
                targetDistance = distance;
            }

            // go towards healing target if found,
            // otherwise move away from the player
            var path = healingTarget != null
                ? _path.GetPath(GetCellPosition(), healingTarget.GetCellPosition())
                : _meleeMap.RunAway(GetCellPosition(), Vector3Int.zero);

            // end turn if can't find path to target
            if (path == null)
            {
                EndTurn();
                return;
            }

            var nextCell = path.Value;
            // try to heal entity
            var targetEntity = EntityManager.GetEntity(nextCell);
            if (targetEntity)
            {
                if (ShouldHeal(targetEntity))
                    Heal(targetEntity);
                EndTurn();
                return;
            }

            // move otherwise
            MoveTo(nextCell);
        }

        private static bool ShouldHeal(Entity entity)
        {
            return entity.NeedsHealing() && !entity.CompareTag("Player");
        }
    }
}