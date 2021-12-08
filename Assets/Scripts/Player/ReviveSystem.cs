using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveSystem : MonoBehaviour
{
    static float NEED_REVIVAL_TIME = 5.0f;
    private TestHeat.PLAYER_STATE playerState = TestHeat.PLAYER_STATE.ARIVE;
    private float reviveTime;
    // Start is called before the first frame update
    void Start()
    {
        reviveTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // �X�e�[�g���_�E����ԂȂ�
        if(playerState == TestHeat.PLAYER_STATE.DEATH) // ����DEATH�X�e�[�g
        {
            // �h���\�G���A���őh���L�[�������ꂽ��
            if(Input.GetAxisRaw("Jump") != 0.0f)
            {
                if(reviveTime >= NEED_REVIVAL_TIME)
                {
                    // HP��30�ŕ���
                    GetComponent<TestHeat>().m_Hp = 30;
                    // �X�e�[�g�𐶂��Ă����ԂɕύX
                    playerState = TestHeat.PLAYER_STATE.ARIVE;
                    GetComponent<TestHeat>().m_PlayerState = playerState;
                    GetComponent<PlayerController>().SetPlayerState(playerState);
                    Debug.Log("Revive");
                    // �^�C�}�[�����Z�b�g
                    reviveTime = 0.0f;
                }
                reviveTime += Time.deltaTime;
            }
        }
    }

    public void SetPlayerState(TestHeat.PLAYER_STATE nowState)
    {
        playerState = nowState;
    }
}
