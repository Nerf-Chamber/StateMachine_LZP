using UnityEngine;

[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(RotationBehaviour))]
[RequireComponent(typeof(HurtBehaviour))]
public abstract class Character : MonoBehaviour, IHurtable
{
    [SerializeField] protected MoveBehaviour _move;
    [SerializeField] protected RotationBehaviour _rotation;
    [SerializeField] protected HurtBehaviour _hurt;

    [SerializeField] protected int healthPoints;

    protected virtual void Awake()
    {
        _move = GetComponent<MoveBehaviour>();
        _rotation = GetComponent<RotationBehaviour>();
        _hurt = GetComponent<HurtBehaviour>();
    }

    public virtual void Hurt(int damage)
    {
        healthPoints -= damage;
        _hurt.Hurt(ref healthPoints, damage);
    }
}