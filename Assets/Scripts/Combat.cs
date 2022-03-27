using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private int damage;

    public void Attack(Entity target)
    {
        print($"<color=yellow>{name}</color> is attacking <color=yellow>{target}</color>");
        target.GetComponent<Health>().Hurt(damage);
    }
}