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

    public int HP_WHEN_REVIVED = 50;
    public int NEXT_DOWN_HP = 70;

    public Animator animator;

    [SerializeField] GameObject m_playerModel;
    // Start is called before the first frame update
    void Start()
    {
        reviveTime = 0.0f;
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
            if (GameObject.Find("Fire").GetComponent<ReviveArea>().GetOnFireArea() == true)
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

                    // 生存数を増やす
                    GameObject.Find("VictoryJudgement").GetComponent<VictoryJudgement>().IsPlayerRevive();

                    // Start Animation
                    animator.SetBool("Down", false);

                    Debug.Log("Revive");
                    // タイマーをリセット
                    reviveTime = 0.0f;

                    //Quaternion nowRot = m_playerModel.transform.rotation;
                    //nowRot *= Quaternion.Euler(-90.0f, 0.0f, 0.0f);
                    //m_playerModel.transform.rotation = nowRot;
                    // モデルの位置調整
                    m_playerModel.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }
        }
    }
    public void SetPlayerState(TestHeat.PLAYER_STATE nowState)
    {
        playerState = nowState;
    }
}
