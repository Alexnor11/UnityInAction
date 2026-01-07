using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6f;
    public float jumpSpeed = 15f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10f;
    public float minFall = -1.5f;

    private CharacterController charController;
    private float vertSpeed;

    private void Start()
    {
        vertSpeed = minFall;
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 movment = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        if(horInput != 0 || vertInput != 0)
        {
            Vector3 right = target.right;
            Vector3 forward = Vector3.Cross(right, Vector3.up);
            movment = (right * horInput) + (forward * vertInput);

            movment *= moveSpeed;
            movment = Vector3.ClampMagnitude(movment, moveSpeed);

            Quaternion direction = Quaternion.LookRotation(movment);
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                direction, rotSpeed * Time.deltaTime);           
            }
        if (charController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                vertSpeed = jumpSpeed;
            }
            else
            {
                vertSpeed = minFall;
            }
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }
        }
        movment.y = vertSpeed;

        movment *= Time.deltaTime;
        charController.Move(movment);
    }
}
