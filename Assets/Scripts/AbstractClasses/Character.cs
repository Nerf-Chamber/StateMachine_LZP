using UnityEngine;

[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(RotationBehaviour))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] protected MoveBehaviour _move;
    [SerializeField] protected RotationBehaviour _rotation;

    [SerializeField] protected float speed;
    [SerializeField] protected float rotationSpeed;

    protected virtual void Awake()
    {
        _move = GetComponent<MoveBehaviour>();
        _rotation = GetComponent<RotationBehaviour>();
    }
}