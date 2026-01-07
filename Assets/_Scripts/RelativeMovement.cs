using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float rotSpeed = 15.0f;

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

            Quaternion direction = Quaternion.LookRotation(movment);
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                direction, rotSpeed * Time.deltaTime);
        }
    }
}
