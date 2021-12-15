using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

using Photon.Pun;

public class RedHpSliderControl : MonoBehaviourPunCallbacks
{
    //HPスライダー
    [SerializeField] Slider m_HPSlider;

    //赤い水スライダー
    [SerializeField] Slider m_RedHPSlider;

    //エフェクトボリューム
    [SerializeField] Volume m_Volume;
    [SerializeField] GameObject m_VolumeObj;
    private Vignette m_Vignette;
    

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
        if (photonView.IsMine)
        {
            m_VolumeObj.SetActive(true);
        }
        else
        {
            m_VolumeObj.SetActive(false);
        }

        //値が違ったら値を入れる
        if (m_Value != m_HPSlider.value)
        {
            if (m_Value <= m_HPSlider.value)
            {
                m_RedHPSlider.value = m_HPSlider.value;
            }
            
            m_Value = m_HPSlider.value;
            m_RedValue = m_RedHPSlider.value;

            

            //カウントを設定する
            m_ChangeCount = CHANGE_WAIT_TIME;

            //フラグをfalseにする
            //m_SliderMoveFrag = false;

            m_Volume.profile.TryGet<Vignette>(out m_Vignette);

            m_Vignette.intensity.value = 1 - m_Value / 100;

        }
        

        //カウントを減らす
        m_ChangeCount -= Time.deltaTime;

        //0を下回ったら
        if (m_ChangeCount <= 0)
        {
            m_SliderMoveFrag = true;
        }

        if (m_SliderMoveFrag)
        {
            if (m_RedHPSlider.value <= m_Value)
            {
                m_SliderMoveFrag = false;
            }
            else
            {
                m_RedHPSlider.value -= (m_RedValue - m_Value) / CHANGE_FRAME;
            }
        }
    }
}
