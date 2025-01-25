using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform Capsule;
    public float mouseSensitivity = 1000f;

    float xRotation = 0f;
    public float zTilt = 0f;
    public float tiltAngle = 10f;
    public float tiltSpeed = 5f;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Capsule.Rotate(Vector3.up * inputX);

        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Tilt();

    }

    void Tilt()
    {
        if (Input.GetKey(KeyCode.A))
        {
            zTilt = Mathf.Lerp(zTilt, tiltAngle, Time.deltaTime * tiltSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            zTilt = Mathf.Lerp(zTilt, -tiltAngle, Time.deltaTime * tiltSpeed);
        }
        else
        {
            zTilt = Mathf.Lerp(zTilt, 0f, Time.deltaTime * tiltSpeed);
        }
        transform.localRotation = Quaternion.Euler(xRotation, 0f, zTilt);
    }
}
