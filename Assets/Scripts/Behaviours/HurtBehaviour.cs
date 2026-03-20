using UnityEngine;

public class HurtBehaviour : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float forcePower;
    [SerializeField] private float timeBetweenDamage;
    private bool canTakeDamage = true;

    private void Awake() { _rb = GetComponent<Rigidbody>(); }
    public void Hurt(ref int health, int damage)
    {
        if (canTakeDamage)
        {
            _rb.AddForce(Vector3.up * forcePower);
            health -= damage;
            canTakeDamage = false;
            Invoke(nameof(ResetCanTakeDamage), timeBetweenDamage);
        }
    }
    private void ResetCanTakeDamage() => canTakeDamage = true;
}