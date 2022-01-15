using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using UnityStandardAssets.Characters.FirstPerson;

public class TestHeat : MonoBehaviourPunCallbacks
{
    //状態
    public enum PLAYER_STATE
    {
        ARIVE = 0,  //生きている
        DAWN,       //ダウン
        DEATH,      //死亡
    };

    //体力
    public int m_Hp = 100;

    //ダウン状態での体力
    public float m_DownStateHp = 100.0f;

    private static float SUBSTRACTION_DOWN_STATE_HP = 5;
    private float m_DownStateHpSubCount = 0;
    private static float SUBSTRACTION_TIME = 3f;

    //プレイヤーの状態
    public PLAYER_STATE m_PlayerState = PLAYER_STATE.ARIVE;

    //体力のスライダー
    [SerializeField]Slider m_HPSlider;
    [SerializeField] Image m_sliderImage;
    [SerializeField] GameObject m_HpSliderObj;
    [SerializeField] GameObject m_RedHpSliderObj;
    [SerializeField] Slider m_RedHpSlider;

    [SerializeField] GameObject m_playerModel;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            m_HpSliderObj.SetActive(true);
            m_RedHpSliderObj.SetActive(true);

            if(GameObject.Find("VictoryJudgement").GetComponent<VictoryJudgement>().GetIsGameFinish() == true
                && m_PlayerState != PLAYER_STATE.ARIVE)
            {
                photonView.RPC(nameof(Change2), RpcTarget.All);
                GetComponent<ReviveSystem>().SetPlayerState(m_PlayerState);
                GetComponent<PlayerController>().SetPlayerState(m_PlayerState);
                GetComponent<FirstPersonController>().SetNowPlayerState((int)m_PlayerState);
            }

            switch (m_PlayerState)
            {
                case PLAYER_STATE.ARIVE:
                    {
                        m_HPSlider.value = m_Hp;

                        //体力がなくなったら状態を変える
                        //change your state.
                        if (m_Hp <= 0)
                        {
                            photonView.RPC(nameof(Change), RpcTarget.All);
                            Debug.Log("Change");
                            GetComponent<ReviveSystem>().SetPlayerState(m_PlayerState);
                            GetComponent<PlayerController>().SetPlayerState(m_PlayerState);
                            GetComponent<FirstPersonController>().SetNowPlayerState((int)m_PlayerState);
                            Debug.Log("Dawn");
                            m_RedHpSlider.value = 0;
                            m_sliderImage.color = Color.yellow;

                            // プレイヤーのモデルを倒す
                            Quaternion nowRot = m_playerModel.transform.rotation;
                            nowRot *= Quaternion.Euler(90.0f, 0.0f, 0.0f);
                            m_playerModel.transform.rotation = nowRot;
                            // モデルの位置調整
                            m_playerModel.transform.localPosition = new Vector3(2.0f, 1.0f, -3.5f);
                        }
                        break;
                    }
                case PLAYER_STATE.DAWN:
                    {
                        m_DownStateHpSubCount += Time.deltaTime;

                        if(m_DownStateHpSubCount >= SUBSTRACTION_TIME)
                        {
                            m_DownStateHpSubCount = 0;
                            m_DownStateHp -= SUBSTRACTION_DOWN_STATE_HP;
                        }
                        m_HPSlider.value = m_DownStateHp;

                        //遷移したかの確認
                        photonView.RPC(nameof(Check), RpcTarget.All);

                        //ダウン状態での体力がなくなったら状態を変える
                        if (m_DownStateHp <= 0)
                        {
                            photonView.RPC(nameof(Change2), RpcTarget.All);
                            GetComponent<ReviveSystem>().SetPlayerState(m_PlayerState);
                            GetComponent<PlayerController>().SetPlayerState(m_PlayerState);
                            GetComponent<FirstPersonController>().SetNowPlayerState((int)m_PlayerState);
                            GameObject.Find("VictoryJudgement").GetComponent<VictoryJudgement>().IsPlayerDead();
                        }
                        break;
                    }
                case PLAYER_STATE.DEATH:
                    {
                        break;
                    }
            }

        }
        else
        {
            m_HpSliderObj.SetActive(false);
            m_RedHpSliderObj.SetActive(false);
        }
    }

    public int GetOwnerId()
    {
        return photonView.OwnerActorNr;
    }

    public void HpDowm(int power)
    {
        //水の威力分体力を下げる
        //Lower your HP　by the Power of water.
        photonView.RPC(nameof(HpDowmRPC), RpcTarget.All, power);
    }


    //体力を下げる
    [PunRPC]
    private void HpDowmRPC(int power)
    {
        //水の威力分体力を下げる
        //Lower your HP　by the Power of water.
        if (photonView.IsMine)
        {
            switch (m_PlayerState)
            {
                case PLAYER_STATE.ARIVE:
                    {
                        m_Hp -= power;
                        break;
                    }
                case PLAYER_STATE.DAWN:
                    {
                        m_DownStateHp -= (float)power / 2;
                        break;
                    }
            }
        }
    }
    //------------------------------------------------


    //確認用
    //TEST script 
    [PunRPC]
    void Check()
    {
        //this.gameObject.transform.Rotate(0, 0, 180);
    }
    [PunRPC]
    void Change()
    {
        if (m_Hp <= 0)
        {
            m_PlayerState = PLAYER_STATE.DAWN;
            m_Hp = 0;
        }
    }
    [PunRPC]
    void Change2()
    {      
        m_PlayerState = PLAYER_STATE.DEATH;            
    }
}
