using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private float speedMult = 1;
    private Vector3 originPosition;
    private Vector3 originEulerAngles;

    public Transform pivot;
    public float speed = 1;
    public float speedMultiplier = 3;

    private void Start()
    {
        originPosition = transform.localPosition;
        originEulerAngles = transform.localEulerAngles;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMult = speedMultiplier;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMult = 1;
        }

        if (Input.GetAxis("Horizontal") < -.1f)
        {
            transform.Translate(Vector3.left * speed * speedMult);
        }
        else if (Input.GetAxis("Horizontal") > .1f)
        {
            transform.Translate(Vector3.right * speed * speedMult);
        }

        if (Input.GetAxis("Vertical") < -.1f)
        {
            transform.Translate(Vector3.down * speed * speedMult);
        }
        else if (Input.GetAxis("Vertical") > .1f)
        {
            transform.Translate(Vector3.up * speed * speedMult);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.forward * speed * speedMult);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.back * speed * speedMult);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.localPosition = originPosition;
            transform.localEulerAngles = originEulerAngles;
        }

        if (pivot != null)
            transform.LookAt(pivot);
    }
}
