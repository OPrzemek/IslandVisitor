using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] [Min(0)] private float speed = 10f;
    [SerializeField] [Min(0)] private float jumpForce = 6f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] [Min(0)] private float checkRadius = 0.25f;
    [SerializeField] [Min(1)] private float sprintMultiplier = 1.5f;
    [SerializeField] [Min(0)] private float acceleration = 5f;
    [SerializeField] [Min(0)] private float brakingForce = 3.2f;
    [SerializeField] [Min(0)] private float crouchHeight = 1f;
    [SerializeField] [Min(0)] private float normalHeight = 2f;
    [SerializeField] [Range(30,110)] private float fieldOfView = 60f;
    [SerializeField] private Animator animator;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Camera.main.fieldOfView = fieldOfView;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 moveBy = transform.right * x + transform.forward * z;
        float maxSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) && z == 1 && IsOnGround())
        {
            maxSpeed *= sprintMultiplier;
        }
        if (playerRb.velocity.magnitude < maxSpeed)
        {
            playerRb.AddForce(moveBy.normalized * acceleration);
        }
        float standingThreshold = 0.01f;
        if (playerRb.velocity.magnitude <= speed * standingThreshold)
        {
            animator.ResetTrigger("run");
            animator.ResetTrigger("walk");
            animator.SetTrigger("idle");
        }
        else
        {
            if(Input.GetKey(KeyCode.LeftShift) && z == 1 && IsOnGround())
            {
                animator.ResetTrigger("idle");
                animator.ResetTrigger("walk");
                animator.SetTrigger("run");
            }
            else
            {
                animator.ResetTrigger("idle");
                animator.ResetTrigger("run");
                animator.SetTrigger("walk");
            }
        }
        if (moveBy == Vector3.zero && playerRb.velocity.magnitude > 0)
        {
            if (playerRb.velocity.magnitude > brakingForce && IsOnGround())
                playerRb.AddForce(playerRb.velocity.normalized * -1 * brakingForce);
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        Vector3 newScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);
        if (Input.GetKey(KeyCode.C) && !Input.GetKey(KeyCode.LeftShift))
        {
            newScale.y = crouchHeight;
        }
        transform.localScale = newScale;
    }
    private bool IsOnGround()
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, checkRadius, groundLayer);
        if (colliders.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
