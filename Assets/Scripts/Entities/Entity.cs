using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public delegate void TurnEnded(Entity entity);

    public delegate void MovementEnded();

    public delegate void EntityDied(Entity entity);

    public class Entity : MonoBehaviour
    {
        private const float DistanceThreshold = 0.1f;
        private const float Speed = 16f;
        [SerializeField] private int maxHealth;
        [SerializeField] private int damage;
        [SerializeField] private Text healthText;
        protected EntityManager EntityManager;
        private Grid _grid;
        private int _health;
        private bool _moving;
        private Vector3 _targetPosition;
        public EntityDied EntityDied;
        protected MovementEnded MovementEnded;
        public TurnEnded TurnEnded;

        protected virtual void Awake()
        {
            EntityManager = FindObjectOfType<EntityManager>();
            _grid = FindObjectOfType<Grid>();
            _health = maxHealth;
            healthText.text = _health.ToString();
            _targetPosition = transform.position;
        }

        protected virtual void Update()
        {
            UpdatePosition();
        }

        public virtual void OnTurn()
        {
            print($"<color=yellow>{name}</color> at {GetCellPosition()} is starting their turn");
        }

        protected void EndTurn()
        {
            TurnEnded?.Invoke(this);
        }

        protected void Attack(Entity target)
        {
            print($"<color=red>{name}</color> is attacking <color=yellow>{target}</color>");
            target.ChangeHealth(-damage);
        }

        private void UpdatePosition()
        {
            // skip if not moving
            if (!_moving)
                return;
            // calculate new position
            var position = transform.position;
            position = Vector3.Lerp(position, _targetPosition, Speed * Time.deltaTime);
            // stop if close enough to target position
            var distance = Vector3.Distance(position, _targetPosition);
            if (distance <= DistanceThreshold)
            {
                print($"<color=lime>{name}</color> at {GetCellPosition()} has finished moving");
                position = _targetPosition;
                _moving = false;
                MovementEnded?.Invoke();
            }

            // update position
            transform.position = position;
        }

        protected void MoveTo(Vector3Int cellPosition)
        {
            _targetPosition = _grid.CellToWorld(cellPosition) + _grid.cellSize * 0.5f;
            _moving = true;
        }

        private void ChangeHealth(int amount)
        {
            _health += amount;
            // prevent health from going out of bounds
            _health = Mathf.Clamp(_health, 0, maxHealth);
            healthText.text = _health.ToString();
            if (_health <= 0)
                Die();
        }

        private void Die()
        {
            print($"<color=yellow>{name}</color> has died");
            EntityDied?.Invoke(this);
            // destroy self
            Destroy(gameObject);
        }

        public Vector3Int GetCellPosition()
        {
            return _grid.WorldToCell(_targetPosition);
        }
    }
}