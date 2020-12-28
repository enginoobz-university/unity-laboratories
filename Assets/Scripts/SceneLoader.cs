using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Michsky.UI.ModernUIPack;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] CustomDropdown dropdown;
    [SerializeField] int initialSceneId;
    SortedDictionary<int, string> scenes; // build index and name

    private void Start()
    {
        Screen.fullScreen = true;

        // init task names
        scenes = new SortedDictionary<int, string>(){
            {0, "Lab 1: Table"},
            {1, "Lab 1: Train"},
        };

        if (dropdown == null) return;

        // init dropdown options
        foreach (string taskName in scenes.Values)
        {
            //dropdown.options.Add(new Dropdown.OptionData(taskName));
            dropdown.CreateNewItemFast(taskName, null);
        }
        dropdown.SetupDropdown();

        // load initial dropdown option
        dropdown.ChangeDropdownInfo(initialSceneId);
        SceneManager.LoadScene(initialSceneId);
    }

    public void HandleDropdown(int option)
    {
        SceneManager.LoadScene(option);
        //LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
