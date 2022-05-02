using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities
{
    public class EntityCanvas : MonoBehaviour
    {
        private readonly List<Canvas> _canvasList = new List<Canvas>();
        [SerializeField] private InputActionReference showAction;
        private bool _show;

        private void Awake()
        {
            GetComponent<EntityManager>().EntitiesUpdated += OnEntitiesUpdated;
            showAction.action.performed += OnShow;
            showAction.action.canceled += OnShow;
        }

        private void OnEntitiesUpdated(List<Entity> entities)
        {
            // update the list of canvases
            _canvasList.Clear();
            foreach (var entity in entities)
            {
                _canvasList.Add(entity.GetComponentInChildren<Canvas>());
            }
        }

        private void OnEnable()
        {
            showAction.action.Enable();
        }

        private void OnDisable()
        {
            showAction.action.Disable();
        }

        private void OnDestroy()
        {
            showAction.action.performed -= OnShow;
            showAction.action.canceled -= OnShow;
        }

        private void OnShow(InputAction.CallbackContext obj)
        {
            // _show = obj.performed;  // hold
            // toggle
            if (obj.performed)
                _show = !_show;
            // enable canvases
            foreach (var canvas in _canvasList)
            {
                canvas.enabled = _show;
            }
        }
    }
}