using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight=1f;

    bool crouching = false;
    float crouchTimer = 1f;
    bool lerpCrouch = false;
    void Start()
    {
        controller=GetComponent<CharacterController>();
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded=controller.isGrounded;
        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p = p * p; ;
            if(crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if(p>1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

    }
 
    public void ProcessMove(Vector2 input)
    {
        if(!PlayerUI.paused && !PlayerUI.notebookState && !PlayerUI.inTerminal)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
            
            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
                playerVelocity.y = -2f;

            controller.Move(playerVelocity * Time.deltaTime);
        }
        
    }

    public void Jump()
    {
        if(isGrounded && !PlayerUI.paused && !PlayerUI.notebookState && !PlayerUI.inTerminal)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
            if(crouching)
                Crouch();
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
}
