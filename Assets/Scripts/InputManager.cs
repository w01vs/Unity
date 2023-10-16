using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerLook look;
    private PlayerMotor motor;
    private Gun gun;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        onFoot.Jump.performed += ctx => motor.Jump();
        look = GetComponent<PlayerLook>();
        gun = GameObject.Find("BasicGun").GetComponent<Gun>();
        onFoot.SprintStart.performed += ctx => motor.SprintPressed();
        onFoot.SprintFinish.performed += ctx => motor.SprintReleased();
        onFoot.CrouchStart.performed += ctx => motor.CrouchPressed();
        onFoot.CrouchFinish.performed += ctx => motor.CrouchReleased();
        onFoot.Inspect.performed += ctx => gun.Inspect();
        onFoot.Reload.performed += ctx => gun.Reload();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
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
