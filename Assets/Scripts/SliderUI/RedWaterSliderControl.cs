using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedWaterSliderControl : MonoBehaviour
{
    //���X���C�_�[
    [SerializeField] Slider m_WaterSlider;

    //�Ԃ����X���C�_�[
    [SerializeField] Slider m_RedWaterSlider;

    //�l�ۑ�
    float m_Value;
    float m_RedValue;

    //�J�E���g
    float m_ChangeCount;

    //�ω��܂ł̎���
    const float CHANGE_WAIT_TIME = 1;

    //�Q�[�W�������t���O
    bool m_SliderMoveFrag;

    //�Q�[�W����������
    const int CHANGE_FRAME = 90;

    // Start is called before the first frame update
    void Start()
    {
        m_Value = 0;
        m_ChangeCount = 0;
        m_SliderMoveFrag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�l���������l������
        if(m_Value != m_WaterSlider.value)
        {
            m_Value = m_WaterSlider.value;
            m_RedValue = m_RedWaterSlider.value;

            //�J�E���g��ݒ肷��
            m_ChangeCount = CHANGE_WAIT_TIME;

            //�t���O��false�ɂ���
            m_SliderMoveFrag = false;
        }
       
        //�J�E���g�����炷
        m_ChangeCount -= Time.deltaTime;

        //0�����������
        if (m_ChangeCount <= 0)
        {
            m_SliderMoveFrag = true;
        }

        if(m_SliderMoveFrag)
        {
            if(m_RedWaterSlider.value <= m_Value)
            {
                m_SliderMoveFrag = false;
            }
            else
            {
                m_RedWaterSlider.value -= (m_RedValue - m_Value) / CHANGE_FRAME;
            }           
        }
    }
}
