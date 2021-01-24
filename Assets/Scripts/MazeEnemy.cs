using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEnemy : MonoBehaviour
{
    [SerializeField] float moveVelocity = 3;
    [SerializeField] bool horizontalMovement = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (horizontalMovement)
        {
            transform.position += new Vector3(-moveVelocity * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(0, 0, -moveVelocity * Time.deltaTime);
        }
    }

    // set flag to avoid multiple collisions at the same time
    private bool hasCollide = false;
    private void LateUpdate()
    {
        hasCollide = false;
    }
    private void OnCollisionEnter(Collision other)
    {

        if (!other.gameObject.CompareTag("Ground") && !hasCollide)
        {
            hasCollide = true;
            moveVelocity = -moveVelocity;
        }
    }
}
