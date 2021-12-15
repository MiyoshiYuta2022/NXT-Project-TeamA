using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    //�̗͂̃X���C�_�[
    [SerializeField]Slider m_HPSlider;
    [SerializeField] GameObject m_HpSliderObj;
    [SerializeField] GameObject m_RedHpSliderObj;


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
                            GetComponent<ReviveSystem>().SetPlayerState(m_PlayerState);
                            GetComponent<PlayerController>().SetPlayerState(m_PlayerState);
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
                        GetComponent<ReviveSystem>().SetPlayerState(m_PlayerState);
                        GetComponent<PlayerController>().SetPlayerState(m_PlayerState);
                        GameObject.Find("VictoryJudgement").GetComponent<VictoryJudgement>().IsPlayerDead();
                        break;
                    }
                case PLAYER_STATE.DEATH:
                    {
                        break;
                    }
            }

            m_HPSlider.value = m_Hp;
        }
        else
        {
            m_HpSliderObj.SetActive(false);
            m_RedHpSliderObj.SetActive(false);
        }
    }
    

    void OnTriggerEnter(Collider other)
    {
        //���ɓ���������
        //be exposed to water        
        if (other.gameObject.tag == "Water")
        {
            //�����̐�����Ȃ������珈������
            if (other.GetComponent<WaterMove>().GetOwnerId() != photonView.OwnerActorNr && other.GetComponent<PlayerManager>().playerColor != GetComponent<PlayerManager>().playerColor)
            {
                int power = other.GetComponent<WaterMove>().GetWaterPower();
                photonView.RPC(nameof(HpDowm), RpcTarget.All,power);
            }
        }
        if (other.gameObject.tag == "WaterBomb")
        {
            //�����̐�����Ȃ������珈������
            if (other.GetComponent<WaterBomb>().GetOwnerId() != photonView.OwnerActorNr && other.GetComponent<PlayerManager>().playerColor != GetComponent<PlayerManager>().playerColor)
            {
                int power = other.GetComponent<WaterBomb>().GetWaterPower();
                photonView.RPC(nameof(HpDowm), RpcTarget.All, power);
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
