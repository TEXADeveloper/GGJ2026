using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckLength;
    private bool grounded;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float speedMaskMultiplier;
    private float speedMultiplier = 1;
    private Vector3 direction;

    [Header("Mouse")]
    [SerializeField] private float sensibility;
    [SerializeField] private float maskSensibilityMultiplier;
    private float mouseMultiplier = 1;
    private float pitch;
    private float yaw;
    private Camera cam;

    public bool Grounded() { return grounded; }

    public bool Moving() { return (direction.x != 0 || direction.y != 0); } 

    public void SetMoveInput(Vector2 dir)
    {
        direction = transform.forward * dir.y + transform.right * dir.x;
        direction.Normalize();
    }

    public void SetLookInput(Vector2 dir)
    {
        yaw += dir.x * sensibility;
        pitch -= dir.y * sensibility;
    }

    void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(groundCheckPosition.position, groundCheckLength, groundLayer);

        move();
    }

    void LateUpdate()
    {
        moveCamera();
    }

    private void move()
    {
        //speedMultiplier = mask? 1 : speedMaskMultiplier;
        rb.linearVelocity = new Vector3(direction.x * speed * speedMultiplier, rb.linearVelocity.y, direction.z * speed * speedMultiplier);
    }

    private void moveCamera()
    {
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.localRotation = Quaternion.Euler(0f, yaw, 0f);

        if (cam != null)
        {
            cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckLength);
    }
}
