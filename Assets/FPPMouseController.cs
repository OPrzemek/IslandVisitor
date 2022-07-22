using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPMouseController : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] [Min(1)] private float sensitivity = 400f;
    private float headRotation = 0f;
    [SerializeField] [Min(0)] private float upHeadRotationLimit = 90f;
    [SerializeField] [Min(0)] private float downHeadRotationLimit = -90f;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * -1f;
        transform.Rotate(0f, x, 0f);
        headRotation += y;
        headRotation = Mathf.Clamp(headRotation, downHeadRotationLimit, upHeadRotationLimit);
        cam.localEulerAngles = new Vector3(headRotation, 0f, 0f);
    }
}
