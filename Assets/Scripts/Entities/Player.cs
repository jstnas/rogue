using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities
{
    public class Player : Entity
    {
        private const float InputThreshold = 0.8f;

        [SerializeField] private InputActionReference moveAction;

        // keeps track of when to check for input
        private bool _checkingInput;
        private Floor _floor;
        private Vector2 _input;

        protected override void Awake()
        {
            base.Awake();
            moveAction.action.performed += OnMove;
            _floor = FindObjectOfType<Floor>();
            onMovementEnded.AddListener(EndTurn);
        }

        protected override void Update()
        {
            base.Update();
            // skip if not checking input
            if (!_checkingInput)
                return;
            var newOffset = GetOffset();
            // skip if no key was pressed
            if (newOffset is null)
                return;
            // reset input after it has been checked
            _input = Vector2.zero;
            var newPosition = GetCellPosition() + newOffset.Value;
            // check if can attack entity at new position
            var targetEntity = EntityManager.GetEntity(newPosition);
            if (targetEntity != null)
            {
                Attack(targetEntity);
                _checkingInput = false;
                EndTurn();
                return;
            }

            // skip if can't move to new position
            if (!_floor.IsValidTile(newPosition))
                return;
            MoveTo(newPosition);
            _checkingInput = false;
        }

        private void OnEnable()
        {
            moveAction.action.Enable();
        }

        private void OnDisable()
        {
            moveAction.action.Disable();
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            _input = obj.ReadValue<Vector2>();
            // print(_input); // DEBUG
        }

        public override void OnTurn()
        {
            base.OnTurn();
            _checkingInput = true;
        }

        private Vector3Int? GetOffset()
        {
            if (_input.y > InputThreshold)
                return Vector3Int.up;
            if (_input.x < -InputThreshold)
                return Vector3Int.left;
            if (_input.y < -InputThreshold)
                return Vector3Int.down;
            if (_input.x > InputThreshold)
                return Vector3Int.right;
            return null;
        }
    }
}