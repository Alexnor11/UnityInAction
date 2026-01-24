using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointClickMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6f;
    public float jumpSpeed = 15f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10f;
    public float minFall = -1.5f;
    public float pushForce = 3f;

    public float deceleration = 25f;
    public float targgtBuffer = 1.5f;

    private float curSpeed = 0;
    private Vector3? targetPos; // Определяем это значение как «обнуляемое» с помощью символа «?»

    private CharacterController charController;
    private float vertSpeed;
    private ControllerColliderHit contact;

    private Animator animator;


    private void Start()
    {
        vertSpeed = minFall;
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Направляем луч
                                                                         //в точку щелчка
            RaycastHit mouseHit;
            if(Physics.Raycast(ray, out mouseHit) )
            {
                GameObject hitOject = mouseHit.transform.gameObject;
                if(hitOject.layer == LayerMask.NameToLayer("Ground"))
                {
                    targetPos = mouseHit.point;
                    curSpeed = moveSpeed;
                }
            }
        }
        if(targetPos != null)
        {
            if(curSpeed > moveSpeed * .5f)
            {
                Vector3 adjusredPos = new Vector3(targetPos.Value.x,
                    transform.position.y, targetPos.Value.z);
                Quaternion targetRot = Quaternion.LookRotation(
                    adjusredPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    targetRot, rotSpeed * Time.deltaTime);
            }

            movement = curSpeed * Vector3.forward;
            movement = transform.TransformDirection(movement);

            if(Vector3.Distance(targetPos.Value, transform.position) < targgtBuffer)
            {
                curSpeed -= deceleration * Time.deltaTime;
                if(curSpeed <= 0)
                {
                    targetPos = null;
                }
            }
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

        bool hitGround = false;
        RaycastHit hit;
        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        if (hitGround)
        {
            //if (Input.GetButtonDown("Jump"))
            //{
            //    vertSpeed = jumpSpeed;
            //}
            //else
            //{
                vertSpeed = minFall;
                animator.SetBool("Jumping", false);
            //}
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }

            if (contact != null)
            {
                animator.SetBool("Jumping", true);
            }

            if (charController.isGrounded)
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * moveSpeed;
                }
                else
                {
                    movement += contact.normal * moveSpeed;
                }
            }
        }
        movement.y = vertSpeed;

        movement *= Time.deltaTime;
        charController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}