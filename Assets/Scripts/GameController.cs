using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool escKeyQuit = false;

    private GameObject SettingUIManagerObj;
    private SettingUIManager SettingUIManagerScript;
    private VictoryJudgement VictoryJudgement;

    private void Start()
    {
        //�}�E�X�J�[�\�����\��
        Cursor.visible = false;
        //�}�E�X�J�[�\���̍��W�𒆐S�ŌŒ�
        Cursor.lockState = CursorLockMode.Locked;

        SettingUIManagerObj = GameObject.Find("SettingUIManager");
        SettingUIManagerScript = SettingUIManagerObj.GetComponent<SettingUIManager>();
        VictoryJudgement = GameObject.Find("GameController").GetComponent<VictoryJudgement>();
    }
    void Update()
    {
        if (SettingUIManagerScript.GetMenuMode() == true || VictoryJudgement.GetIsGameFinish() == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
