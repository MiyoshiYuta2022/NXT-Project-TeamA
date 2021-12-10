using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool escKeyQuit = false;

    private GameObject SettingUIManagerObj;
    private SettingUIManager SettingUIManagerScript;

    private void Start()
    {
        //�}�E�X�J�[�\�����\��
        Cursor.visible = false;
        //�}�E�X�J�[�\���̍��W�𒆐S�ŌŒ�
        Cursor.lockState = CursorLockMode.Locked;

        SettingUIManagerObj = GameObject.Find("SettingUIManager");
        SettingUIManagerScript = SettingUIManagerObj.GetComponent<SettingUIManager>();
    }
    void Update()
    {
        ////ESC�L�[�������ƃJ�[�\����
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    //�\����؂�ւ�
        //    Cursor.visible = !Cursor.visible;
        //    // ���݂̃J�[�\�����[�h�ɍ��킹�Đ؂�ւ�
        //    if (Cursor.lockState == CursorLockMode.None) Cursor.lockState = CursorLockMode.Locked;
        //    else Cursor.lockState = CursorLockMode.None;
        //}

        if(SettingUIManagerScript.GetMenuMode() == true)
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
