using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Photon.Pun;

public class ReviveSystem : MonoBehaviourPunCallbacks
{
    static float NEED_REVIVAL_TIME = 5.0f;
    private TestHeat.PLAYER_STATE playerState = TestHeat.PLAYER_STATE.ARIVE;
    public float reviveTime;
    private bool b_onFireArea;

    public int HP_WHEN_REVIVED = 50;
    public int NEXT_DOWN_HP = 70;

    [SerializeField] GameObject m_playerModel;
    // Start is called before the first frame update
    void Start()
    {
        reviveTime = 0.0f;
        b_onFireArea = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        // ステートがダウン状態なら
        if (playerState == TestHeat.PLAYER_STATE.DAWN)
        {
            // 蘇生可能エリア内で蘇生キーが押されたら
            if (b_onFireArea == true)
            {
                reviveTime += Time.fixedDeltaTime;
                if (reviveTime >= NEED_REVIVAL_TIME)
                {
                    // 復活
                    GetComponent<TestHeat>().m_Hp = HP_WHEN_REVIVED;
                    GetComponent<TestHeat>().m_DownStateHp = NEXT_DOWN_HP;

                    // ステートを生きている状態に変更
                    playerState = TestHeat.PLAYER_STATE.ARIVE;
                    GetComponent<TestHeat>().m_PlayerState = playerState;
                    GetComponent<PlayerController>().SetPlayerState(playerState);
                    GetComponent<FirstPersonController>().SetNowPlayerState((int)playerState);

                    Debug.Log("Revive");
                    // タイマーをリセット
                    reviveTime = 0.0f;

                    Quaternion nowRot = m_playerModel.transform.rotation;
                    nowRot *= Quaternion.Euler(-90.0f, 0.0f, 0.0f);
                    m_playerModel.transform.rotation = nowRot;
                }
            }
        }

        b_onFireArea = false;

    }
    public void SetPlayerState(TestHeat.PLAYER_STATE nowState)
    {
        playerState = nowState;
    }

    private void OnTriggerStay(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "Fire")
                b_onFireArea = true;
        }
    }
}
