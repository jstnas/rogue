using UnityEngine;

public delegate void TurnEnded();

public class Entity : MonoBehaviour
{
    public event TurnEnded OnTurnEnded;
    protected Entities Entities;
    protected Floor Floor;
    protected Movement Movement;

    protected virtual void Awake()
    {
        Entities = FindObjectOfType<Entities>();
        Floor = FindObjectOfType<Floor>();
        Movement = GetComponent<Movement>();
        Movement.OnMovementFinished += OnMovementFinished;
    }

    private void OnMovementFinished()
    {
        EndTurn();
    }

    public virtual void OnTurn()
    {
        print($"<color=yellow>{name}</color> is starting their turn");
    }

    protected virtual void EndTurn()
    {
        OnTurnEnded?.Invoke();
    }
}