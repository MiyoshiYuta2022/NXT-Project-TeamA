using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    // CameraSensivity
    public int sens_x = 1500;
    public int sens_y = 1500;

    private CinemachineFreeLook freeLook;

    private GameObject SettingUIManagerObj;
    private SettingUIManager SettingUIManagerScript;

    private CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        this.freeLook = this.GetComponent<CinemachineFreeLook>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        SettingUIManagerObj = GameObject.Find("SettingUIManager");
        SettingUIManagerScript = SettingUIManagerObj.GetComponent<SettingUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingUIManagerScript.GetMenuMode() == true)
        {
            freeLook.m_XAxis.m_MaxSpeed = 0;
            freeLook.m_YAxis.m_MaxSpeed = 0;
        }
        else
        {
            freeLook.m_XAxis.m_MaxSpeed = sens_x;
            freeLook.m_YAxis.m_MaxSpeed = sens_y;
        }
    }
}
