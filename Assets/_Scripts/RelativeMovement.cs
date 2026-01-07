using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6f;

    private CharacterController charController;

    private void Start()
    {
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

        movment *= Time.deltaTime;
        charController.Move(movment);
    }
}
