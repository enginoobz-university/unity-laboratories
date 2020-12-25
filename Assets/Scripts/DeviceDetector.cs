using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeviceDetector : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        if (!Application.isMobilePlatform)
        {
            textMeshPro.text = "Device: not mobile";
        }
        else
        {
            textMeshPro.text = "Device: mobile";
        } 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
