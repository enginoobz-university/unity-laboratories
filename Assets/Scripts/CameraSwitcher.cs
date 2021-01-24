using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] Camera[] cameras;
    [SerializeField] FirstPersonAIO player;
    int activeCameraId;
    // Start is called before the first frame update
    void Start()
    {
        activeCameraId = 0;
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        cameras[activeCameraId].gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            cameras[activeCameraId].gameObject.SetActive(false);

            if (activeCameraId == cameras.Length - 1)
                activeCameraId = 0;
            else
                activeCameraId++;

            cameras[activeCameraId].gameObject.SetActive(true);

            // TODO: reset rotation
            // if not FPS camera, reset rotation and disable looking at 
            if (activeCameraId != 0)
            {
                player.enableCameraMovement = false;
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                player.enableCameraMovement = true;
            }

        }
    }
}
