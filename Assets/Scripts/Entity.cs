using UnityEngine;

public delegate void TurnEndedDelegate();

public class Entity : MonoBehaviour
{
    [SerializeField] protected int damage;
    public event TurnEndedDelegate TurnEndedEvent;
    protected Combat Combat;
    protected Entities Entities;
    protected Floor Floor;
    protected Movement Movement;

    protected virtual void Awake()
    {
        Combat = GetComponent<Combat>();
        Entities = FindObjectOfType<Entities>();
        Floor = FindObjectOfType<Floor>();
        Movement = GetComponent<Movement>();
        Movement.FinishedMovement += OnFinishedMovementFinished;
    }

    private void OnFinishedMovementFinished()
    {
        EndTurn();
    }

    public virtual void OnTurn()
    {
        print($"<color=yellow>{name}</color> is starting their turn");
    }

    protected void EndTurn()
    {
        TurnEndedEvent?.Invoke();
    }
}