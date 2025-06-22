using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    void Awake()
    {
        playerInput=new PlayerInput();
        onFoot = playerInput.onFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        //ctx=callback context
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed+=ctx => motor.Crouch();

    }

    void FixedUpdate()
    {
        //transmitem valorile din actiunile de miscare la playermotor
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
