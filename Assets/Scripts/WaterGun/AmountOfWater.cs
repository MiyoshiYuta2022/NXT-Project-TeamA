using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class AmountOfWater : MonoBehaviourPunCallbacks
{
    //���̗�
    int m_AmountOfWater;

    //���̃{�g���̐�
    int m_BottleNum = 0;

    //���̗ʍő�
    public const int AMOUNT_OF_WATER = 100;

    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��̐��̗ʂ�ݒ�
        m_AmountOfWater = AMOUNT_OF_WATER;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            //�{�g���ɓ���������
            if (other.gameObject.tag == "Bottle")
            {
                //�����Ă���{�g���̐��𑝂₷
                m_BottleNum++;

                //���������I�u�W�F�N�g������
                Destroy(other.gameObject);
            }
        }
    }
    

    //���̗ʎ擾
    public int GetAmountOfWater()
    {
        return m_AmountOfWater;
    }

    //�������炷
    public void DownAmountOfWater(int cost)
    {
        m_AmountOfWater -= cost;
    }
}
    
