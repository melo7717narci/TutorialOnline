using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlScript : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;

    public float speed;
    public float jumpSpeed;
    public float rotateSpeed;
    public float gravity;

    Vector3 targetDirection;
    Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
        RotationControl();
        controller.Move(moveDirection * Time.deltaTime);
    }

    void MoveControl()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Camera.main.transform.right;

        targetDirection = h * right + v * forward;

        if (controller.isGrounded)
        {
            moveDirection = targetDirection * speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else
        {
            float tempy = moveDirection.y;
            moveDirection.y = tempy - gravity * Time.deltaTime;

        }

        if(v > .1 || v < -.1 || h > .1 || h < -.1)
        {
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    void RotationControl()
    {
        Vector3 rotateDirection = moveDirection;
        rotateDirection.y = 0;

        if(rotateDirection.sqrMagnitude > 0.01)
        {
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.Slerp(transform.forward, rotateDirection, step);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
