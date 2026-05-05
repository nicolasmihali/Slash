using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public Vector2 LookValue { get; private set; }

    public event Action JumpEvent;
    public event Action FireEvent;
    public event Action InteractEvent;
    public event Action AttackEvent;
    public event Action BlockEvent;

    public bool SprintInput { get; private set; }
    public bool BlockInput { get; private set; }

    private Controls _controls;

    void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);

        _controls.Player.Enable();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) { FireEvent?.Invoke(); }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) { JumpEvent?.Invoke(); }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) { InteractEvent?.Invoke(); }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) { AttackEvent?.Invoke(); }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        //if (context.performed) { BlockEvent?.Invoke(); }
        BlockInput = context.ReadValueAsButton();
    }

    public void OnLook(InputAction.CallbackContext context) 
    {
        LookValue = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        SprintInput = context.ReadValueAsButton();
    }
}
