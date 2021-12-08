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
        //マウスカーソルを非表示
        Cursor.visible = false;
        //マウスカーソルの座標を中心で固定
        Cursor.lockState = CursorLockMode.Locked;

        SettingUIManagerObj = GameObject.Find("SettingUIManager");
        SettingUIManagerScript = SettingUIManagerObj.GetComponent<SettingUIManager>();
    }
    void Update()
    {
        ////ESCキーを押すとカーソルを
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    //表示を切り替え
        //    Cursor.visible = !Cursor.visible;
        //    // 現在のカーソルモードに合わせて切り替え
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
