using UnityEngine;

public delegate void EntityDied(Entity entity);

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    private Entity _entity;
    private int _health;

    private void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    public event EntityDied EntityDied;

    public void Hurt(int damage)
    {
        _health -= damage;
        // prevent health from going negative
        _health = Mathf.Max(_health, 0);
        if (_health <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        _health += amount;
        // limit health to maximum
        _health = Mathf.Min(_health, maxHealth);
    }

    private void Die()
    {
        print($"<color=yellow>{name}</color> has died");
        EntityDied?.Invoke(_entity);
        // destroy self
        Destroy(gameObject);
    }
}