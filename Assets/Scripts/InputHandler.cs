using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public Vector2 LookInput { get; private set; } = Vector2.zero;

    Player_movement _input;

    private void OnEnable()
    {
        _input = new Player_movement();
        _input.Player.Enable();

        _input.Player.Move.performed += SetMove;
        _input.Player.Move.canceled += SetMove;

        _input.Player.Look.performed += SetLook;
        _input.Player.Look.canceled += SetLook;

    }

    private void OnDisable()
    {
        _input.Player.Move.performed += SetMove;
        _input.Player.Move.canceled += SetMove;

        _input.Player.Look.performed += SetLook;
        _input.Player.Look.canceled += SetLook;

        _input.Player.Disable();
    }

    private void SetLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    private void SetMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
}
