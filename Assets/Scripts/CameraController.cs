using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private float rotationX, rotationY;

    [SerializeField]
    private float headRotationLimit = 60f;

    private bool isCursorLocked = true;
    // Start is called before the first frame update
    void Start()
    {
        //Make sure the cursor stays in the center of the screen
        //Change the lock state of our cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        // Change the lock mode when left control is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCursorLocked = !isCursorLocked;
            if (isCursorLocked) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }

        // Don't move the camera if the cursor is not locked
        if (Cursor.lockState == CursorLockMode.None) return;

        //look up and down is based on the x-axis rotation
        rotationX += Input.GetAxis("Mouse Y");
        //look left and right is based on the y-axis rotation
        rotationY += Input.GetAxis("Mouse X");

        //Limit the value of our lookup/down based on the head rotation value
        rotationX = Mathf.Clamp(rotationX, -headRotationLimit, headRotationLimit);
    }

    private void LateUpdate()
    {
        //rotate the camera to face our mouse direction
        Quaternion rotation = Quaternion.Euler(-rotationX, rotationY, 0);
        transform.rotation = rotation;

        //make the camera follow the target
        transform.position = target.transform.position;
        //rotate the target to face the camera direction
        target.transform.rotation = Quaternion.Euler(
            target.transform.rotation.x,
            rotationY,
            target.transform.rotation.z);
    }
}
