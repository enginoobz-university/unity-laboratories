using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float rotateStep = 90;
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, verticalInput * speed * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.Q))
        {
            transform.Rotate(0, rotateStep, 0);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            transform.Rotate(0, -rotateStep, 0);
        }
    }
}