using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraControl : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject MainCamera;

    private Slider SensivityX;
    private Slider SensivityY;

    // CameraSensivity
    public float sens_x = 2.0f;
    public float sens_y = 2.0f;

    private GameObject SettingUIManagerObj;
    private SettingUIManager SettingUIManagerScript;

    private FirstPersonController fps;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine == false)
        {
            MainCamera.SetActive(false);
        }

        SettingUIManagerObj = GameObject.Find("SettingUIManager");
        SettingUIManagerScript = SettingUIManagerObj.GetComponent<SettingUIManager>();

        fps = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SensivityX == null)
        {
            if (SettingUIManagerScript.SettingPanel.activeSelf == true)
            {
                SensivityX = GameObject.Find("SensivityX").GetComponent<Slider>();
                SensivityY = GameObject.Find("SensivityY").GetComponent<Slider>();
            }
        }
        
        if(SettingUIManagerScript.SettingPanel.activeSelf == false)
        {
            if(SensivityX != null)
            {
                SensivityX = null;
                SensivityY = null;
            }
        }

        if (SensivityX != null)
        {
            sens_x = SensivityX.value;
            sens_y = SensivityY.value;
        }

        sens_x = float.Parse(sens_x.ToString("N1"));
        sens_y = float.Parse(sens_y.ToString("N1"));

        fps.GetMouseLook().XSensitivity = sens_x;
        fps.GetMouseLook().YSensitivity = sens_y;
    }
}
