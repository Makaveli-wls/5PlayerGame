using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header ("Movement Variables")]
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Vector3 velocity;

    public float gravity;
    public float speed;
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float jumpForce;
    public float groundDist;
    public float standingHeight;
    public float crouchingHeight;

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        //JUMP

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        //SPRINT

        if(Input.GetButtonDown("Sprint"))
        {
            speed = sprintSpeed;
        }
        else if(Input.GetButtonUp("Sprint"))
        {
            speed = walkSpeed;
        }


        //CROUCH

        if(Input.GetButtonDown("Crouch"))
        {
            speed = crouchSpeed;
            controller.height -= crouchingHeight;
           // playerCol.height -= crouchingHeight;
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            speed = walkSpeed;
            controller.height = standingHeight;
            //playerCol.height = standingHeight;
        }

    }
}
