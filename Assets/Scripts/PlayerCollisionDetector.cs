using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Diamond":
                Destroy(other.gameObject);
                MazeGame.Instance.OnDiamondCollected();
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                MazeGame.Instance.Loose();
                Time.timeScale = 0;
                // GetComponent<FirstPersonAIO>().enabled = false;
                break;
        }
    }
}
