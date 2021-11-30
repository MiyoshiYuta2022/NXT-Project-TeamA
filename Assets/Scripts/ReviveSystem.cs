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
        // ステートがダウン状態なら
        if(playerState == TestHeat.PLAYER_STATE.DEATH) // 仮でDEATHステート
        {
            // 蘇生可能エリア内で蘇生キーが押されたら
            if(Input.GetAxisRaw("Jump") != 0.0f)
            {
                if(reviveTime >= NEED_REVIVAL_TIME)
                {
                    // HPを30で復活
                    GetComponent<TestHeat>().m_Hp = 30;
                    // ステートを生きている状態に変更
                    playerState = TestHeat.PLAYER_STATE.ARIVE;
                    GetComponent<TestHeat>().m_PlayerState = playerState;
                    GetComponent<PlayerController>().SetPlayerState(playerState);
                    Debug.Log("Revive");
                    // タイマーをリセット
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
