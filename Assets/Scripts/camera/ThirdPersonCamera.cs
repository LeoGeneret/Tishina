using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = 1.2f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    // Y Axis
    float yaw;
    // X Axis
    float pitch;

    private bool camLockStatus = false;

    void Start()
    {
        // Lock Cursor
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        if (!camLockStatus)
        {
            yaw += Input.GetAxis("Mouse X");
            pitch -= Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

            transform.eulerAngles = currentRotation;

            bool aiming = Input.GetMouseButton(1);

            if (aiming)
            {
                Vector3 aimPos = new Vector3(0.5f, target.transform.localPosition.y, target.transform.localPosition.z);

                target.transform.localPosition = Vector3.Lerp(target.transform.localPosition, aimPos, Time.deltaTime * 1.3f);
            }
            else
            {
                Vector3 startPos = new Vector3(0f, target.transform.localPosition.y, target.transform.localPosition.z);

                target.transform.localPosition = Vector3.Lerp(target.transform.localPosition, startPos, Time.deltaTime * 1.3f);
            }

            //Vector3 pos = (aiming) ? new Vector3(target.position.y + 0.5f, target.position.y, target.position.z) : target.position;

            transform.position = target.position - transform.forward * dstFromTarget;

            //if (Physics.Linecast(transform.position, cam))
        }
    }

    public bool getCamLockStatus()
    {
        return camLockStatus;
    }

    public void setCamLockStatus(bool status)
    {
        camLockStatus = status;
    }
}
