using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class TestHeat : MonoBehaviourPunCallbacks
{
    //���
    public enum PLAYER_STATE
    {
        ARIVE = 0,  //�����Ă���
        DAWN,       //�_�E��
        DEATH,      //���S
    };

    //�̗�
    public int m_Hp = 100;  

    //�v���C���[�̏��
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
                    //�̗͂��Ȃ��Ȃ������Ԃ�ς���
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
                    //�J�ڂ������̊m�F
                    photonView.RPC(nameof(Check), RpcTarget.All);

                    //�ς���i���j
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
        //���ɓ���������
        //be exposed to water        
        if (other.gameObject.tag == "Water")
        {
            //�����̐�����Ȃ������珈������
            if (other.GetComponent<WaterMove>().GetOwnerId() != photonView.OwnerActorNr)
            {
                int power = other.GetComponent<WaterMove>().GetWaterPower();
                photonView.RPC(nameof(HpDowm), RpcTarget.All,power);
            }
        }

    }

    //�v���C���[�X�e�[�g�̓���
    public PLAYER_STATE GetPlayerState()
    {
        return m_PlayerState;
    }


    //�̗͂�������
    [PunRPC]
    void HpDowm(int power)
    {
        //���̈З͕��̗͂�������
        //Lower your HP�@by the Power of water.
        m_Hp -= power;
    }
    //------------------------------------------------


    //�m�F�p
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