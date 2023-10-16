using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float gravity = -9.8f;
    public const float defaultSpeed = 5f;
    public float currentSpeed;
    public const float sprintSpeed = 8f;
    public const float crouchSpeed = 3f;
    public float jumpHeight = 1f;
    private bool lerpCrouch;
    private bool crouching;

    private float crouchTimer;

    private bool sprinting;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        IfGrounded();
        LerpCrouch();
    }

    private void IfGrounded()
    {
        if (isGrounded)
        {
            if (sprinting && !crouching)
            {
                currentSpeed = sprintSpeed;
            }
            else if (crouching)
            {
                currentSpeed = crouchSpeed;
            }
        }
    }

    private void LerpCrouch()
    {
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void SprintPressed()
    {
        sprinting = true;
        if(isGrounded)
        {
            currentSpeed = sprintSpeed;
        }
        if(crouching)
        {
            currentSpeed = crouchSpeed;
        }
    }

    public void SprintReleased()
    {
        sprinting = false;
        if(!sprinting && !crouching)
        {
            currentSpeed = defaultSpeed;
        }
    }

    public void CrouchPressed()
    {
        crouching = true;
        lerpCrouch = true;
        if(isGrounded)
            currentSpeed = crouchSpeed;
    }

    public void CrouchReleased()
    {
        crouching = false;
        lerpCrouch = true;
        if(sprinting && isGrounded)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = defaultSpeed;
        }
    }

}
