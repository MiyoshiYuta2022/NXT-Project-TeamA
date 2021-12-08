using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class SettingUIManager : MonoBehaviour
{
    public GameObject MenuPanel; // MenuPanel
    public GameObject SettingPanel; // SettingPanel

    private bool b_isMenuMode = false; // Is the menu enabled?

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // If esc is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the menu is disabled, enable it.
            if (b_isMenuMode == false) MenuPanel.SetActive(true);
            else MenuPanel.SetActive(false); // Disable if enabled.
        }

        // If either panel is enabled, you are in menu mode.
        if (MenuPanel.activeSelf == true || SettingPanel.activeSelf == true)
            b_isMenuMode = true;
        else b_isMenuMode = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().SetIsMenuMode(b_isMenuMode);

    }

    public void SelectSetting()
    {
        MenuPanel.SetActive(false);
        SettingPanel.SetActive(true);
        GameObject.Find("XValue").GetComponent<SetSensXValue>().SetValue();
        GameObject.Find("YValue").GetComponent<SetSensYValue>().SetValue();
    }

    public void SelectQuit()
    {
        Quit();
    }
    public void SelectClose()
    {
        if (MenuPanel.activeSelf == true) MenuPanel.SetActive(false);
        if(SettingPanel.activeSelf == true)
        {
            SettingPanel.SetActive(false);
            MenuPanel.SetActive(true);
        }
    }

    public bool GetMenuMode()
    {
        return b_isMenuMode;
    }
}
