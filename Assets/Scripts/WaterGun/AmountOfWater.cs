using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class AmountOfWater : MonoBehaviourPunCallbacks
{
    //���̗�
    int m_AmountOfWater;

    //���̗ʍő�
    const int AMOUNT_OF_WATER = 100;

    //�����Ȃ��Ƃ��e�L�X�g
    GameObject m_NornWaterText;

    //���̗�UI
    [SerializeField] Slider m_WaterSlider;
    [SerializeField] GameObject m_WaterSliderObj;
    [SerializeField] Slider m_RedWaterSlider;
    [SerializeField] GameObject m_RedWaterSliderObj;


    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��̐��̗ʂ�ݒ�
        m_AmountOfWater = AMOUNT_OF_WATER;

        m_NornWaterText = GameObject.Find("NornWaterText");
        //�\�����Ȃ���Ԃɂ��Ă���
        m_NornWaterText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            m_WaterSliderObj.SetActive(true);
            m_RedWaterSliderObj.SetActive(true);


            if (m_AmountOfWater <= 0)
            {
                m_NornWaterText.SetActive(true);
            }
            else
            {
                m_NornWaterText.SetActive(false);
            }

            //���̗ʂɉ����ăX���C�_�[�𓮂���
            m_WaterSlider.value = m_AmountOfWater;
        }
        else
        {
            m_WaterSliderObj.SetActive(false);
            m_RedWaterSliderObj.SetActive(false);
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
        m_RedWaterSlider.value = AMOUNT_OF_WATER;
    }
}
    
