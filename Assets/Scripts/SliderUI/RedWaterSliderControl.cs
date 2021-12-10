using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedWaterSliderControl : MonoBehaviour
{
    //水スライダー
    [SerializeField] Slider m_WaterSlider;

    //赤い水スライダー
    [SerializeField] Slider m_RedWaterSlider;

    //値保存
    float m_Value;
    float m_RedValue;

    //カウント
    float m_ChangeCount;

    //変化までの時間
    const float CHANGE_WAIT_TIME = 1;

    //ゲージ動かすフラグ
    bool m_SliderMoveFrag;

    //ゲージ動かす時間
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
        //値が違ったら値を入れる
        if(m_Value != m_WaterSlider.value)
        {
            m_Value = m_WaterSlider.value;
            m_RedValue = m_RedWaterSlider.value;

            //カウントを設定する
            m_ChangeCount = CHANGE_WAIT_TIME;

            //フラグをfalseにする
            m_SliderMoveFrag = false;
        }
       
        //カウントを減らす
        m_ChangeCount -= Time.deltaTime;

        //0を下回ったら
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
