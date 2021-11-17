using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

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

    //プレイヤーの状態
    public PLAYER_STATE m_PlayerState = PLAYER_STATE.ARIVE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_PlayerState)
        {
            case PLAYER_STATE.ARIVE:
                {
                    //体力がなくなったら状態を変える
                    //change your state.
                    if (m_Hp <= 0)
                    {
                        photonView.RPC(nameof(Change), RpcTarget.All);
                        Debug.Log("Change");
                    }
                    break;
                }
            case PLAYER_STATE.DAWN:
                {
                    //遷移したかの確認
                    photonView.RPC(nameof(Check), RpcTarget.All);

                    //変える（仮）
                    photonView.RPC(nameof(Change2), RpcTarget.All);
                    Debug.Log("Dawn");
                    break;
                }
            case PLAYER_STATE.DEATH:
                {
                    break;
                }
        }
    }
    

    void OnTriggerEnter(Collider other)
    {
        //水に当たったら
        //be exposed to water        
        if (other.gameObject.tag == "Water")
        {
            //自分の水じゃなかったら処理する
            if (other.GetComponent<WaterMove>().GetOwnerId() != photonView.OwnerActorNr)
            {
                int power = other.GetComponent<WaterMove>().GetWaterPower();
                photonView.RPC(nameof(HpDowm), RpcTarget.All,power);
            }
        }

    }

    //プレイヤーステートの入手
    public PLAYER_STATE GetPlayerState()
    {
        return m_PlayerState;
    }


    //体力を下げる
    [PunRPC]
    void HpDowm(int power)
    {
        //水の威力分体力を下げる
        //Lower your HP　by the Power of water.
        m_Hp -= power;
    }
    //------------------------------------------------


    //確認用
    //TEST script 
    [PunRPC]
    void Check()
    {
        this.gameObject.transform.Rotate(0, 0, 180);
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
