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
        //�}�E�X�J�[�\�����\��
        Cursor.visible = false;
        //�}�E�X�J�[�\���̍��W�𒆐S�ŌŒ�
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }

        //ESC�L�[�������ƃJ�[�\����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //�\����؂�ւ�
            Cursor.visible = !Cursor.visible;
            // ���݂̃J�[�\�����[�h�ɍ��킹�Đ؂�ւ�
            if (Cursor.lockState == CursorLockMode.None) Cursor.lockState = CursorLockMode.Locked;
            if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        }
    }
}
