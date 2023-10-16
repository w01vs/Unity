using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    public PlayerInput.OnFootActions OnFoot { get; private set; }

    private PlayerLook look;
    private PlayerMotor motor;
    private Gun gun;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        OnFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        OnFoot.Jump.performed += ctx => motor.Jump();
        look = GetComponent<PlayerLook>();
        gun = GameObject.Find("BasicGun").GetComponent<Gun>();
        OnFoot.SprintStart.performed += ctx => motor.SprintPressed();
        OnFoot.SprintFinish.performed += ctx => motor.SprintReleased();
        OnFoot.CrouchStart.performed += ctx => motor.CrouchPressed();
        OnFoot.CrouchFinish.performed += ctx => motor.CrouchReleased();
        OnFoot.Inspect.performed += ctx => gun.Inspect();
        OnFoot.Reload.performed += ctx => gun.Reload();
        OnFoot.Use.performed += ctx => gun.Shoot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(OnFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        OnFoot.Enable();
    }

    private void OnDisable()
    {
        OnFoot.Disable();
    }
}
