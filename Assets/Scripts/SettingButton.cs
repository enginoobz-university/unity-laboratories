using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    bool settingMenuEnabled = false;
    [SerializeField] JoystickSetter[] joystickSetterUis;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ToggleSettingMenu()
    {
        settingMenuEnabled = !settingMenuEnabled;

        // toggle joystick setter uis
        foreach (JoystickSetter ui in joystickSetterUis)
        {
            ui.gameObject.SetActive(settingMenuEnabled);
        }
    }
}
