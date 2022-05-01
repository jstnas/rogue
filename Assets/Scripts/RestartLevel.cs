using UnityEngine;
using UnityEngine.InputSystem;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private InputActionReference restartAction;

    private void Awake()
    {
        restartAction.action.performed += OnRestart;
    }

    private void OnEnable()
    {
        restartAction.action.Enable();
    }

    private void OnDisable()
    {
        restartAction.action.Disable();
    }

    private void OnDestroy()
    {
        restartAction.action.performed -= OnRestart;
    }

    private void OnRestart(InputAction.CallbackContext obj)
    {
        LevelLoader.RestartLevel();
    }
}