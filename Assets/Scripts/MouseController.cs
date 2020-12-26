using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnlockMouse();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMouseLock();
        }
    }

    private void ToggleMouseLock()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            LockMouse();
        }
        else
        {
            UnlockMouse();
        }
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
