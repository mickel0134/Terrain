using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("GroundMovement")]
    public float moveSpeed;
    public float walkingSpeed;
    public float runningSpeed;

    //inputs
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    Vector3 moveDirection;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = walkingSpeed;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Shifts player speed when button is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (moveSpeed == walkingSpeed) moveSpeed = runningSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (moveSpeed == runningSpeed) moveSpeed = walkingSpeed;
        }
    }

    private void MovePlayer()
    {

        //calculate movement direction
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        //on ground
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    void Update()
    {
        MyInput();
        MovePlayer();
        SpeedControl();
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }
}
