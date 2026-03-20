using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character, InputSystem_Actions.IPlayerActions
{
    [SerializeField] private int playerDamage;

    private InputSystem_Actions inputActions;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotationDirection = Vector3.zero;
    private bool attackMode = false;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }
    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void FixedUpdate() 
    {
        _move.Move(moveDirection);
        _rotation.Rotate(rotationDirection);
    }

    // INTERFACE IMPLEMENTATION
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 tempDirection = context.ReadValue<Vector2>();

        moveDirection = new Vector3(tempDirection.x, 0, tempDirection.y);
        if (context.performed)
            rotationDirection = new Vector3(tempDirection.x, 0, tempDirection.y);
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) attackMode = true;
        else if (context.canceled) attackMode = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == nameof(Enemy) && attackMode)
        {
            // Insta kill is the way >:)
            if (collision.collider.TryGetComponent(out IHurtable hurtable))
                hurtable.Hurt(playerDamage);
        }
    }
}