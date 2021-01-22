using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, verticalInput * speed * Time.deltaTime);
    }
}