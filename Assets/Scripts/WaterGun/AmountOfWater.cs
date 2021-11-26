using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class AmountOfWater : MonoBehaviourPunCallbacks
{
    //���̗�
    int m_AmountOfWater;

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

    //���̃����[�h
    public void WaterReload()
    {
        m_AmountOfWater = AMOUNT_OF_WATER;
    }
}
    
