using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions inputActions;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotationDirection = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }
    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Update() 
    {
        _move.Move(moveDirection, speed);
        _rotation.Rotate(rotationDirection, rotationSpeed);
    }

    // INTERFACE IMPLEMENTATION
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 tempDirection = context.ReadValue<Vector2>();

        moveDirection = new Vector3(tempDirection.x, 0, tempDirection.y);
        if (context.performed)
            rotationDirection = new Vector3(tempDirection.x, 0, tempDirection.y);
    }
}