using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    private int _health;

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
        print($"{name} has died");
        // destroy self
        Destroy(gameObject);
    }
}
