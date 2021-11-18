using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }

    private void Start()
    {
        //マウスカーソルを非表示
        Cursor.visible = false;
        //マウスカーソルの座標を中心で固定
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }

        //ESCキーを押すとカーソルを
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //表示を切り替え
            Cursor.visible = !Cursor.visible;
            // 現在のカーソルモードに合わせて切り替え
            if (Cursor.lockState == CursorLockMode.None) Cursor.lockState = CursorLockMode.Locked;
            if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        }
    }
}
