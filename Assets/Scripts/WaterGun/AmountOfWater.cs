using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class AmountOfWater : MonoBehaviourPunCallbacks
{
    //水の量
    int m_AmountOfWater;

    //水の量最大
    const int AMOUNT_OF_WATER = 100;

    //水がないときテキスト
    GameObject m_NornWaterText;

    //水の量UI
    [SerializeField] Slider m_WaterSlider;
    [SerializeField] GameObject m_WaterSliderObj;
    [SerializeField] Slider m_RedWaterSlider;
    [SerializeField] GameObject m_RedWaterSliderObj;


    // Start is called before the first frame update
    void Start()
    {
        //最初の水の量を設定
        m_AmountOfWater = AMOUNT_OF_WATER;

        m_NornWaterText = GameObject.Find("NornWaterText");
        //表示しない状態にしておく
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

            //水の量に応じてスライダーを動かす
            m_WaterSlider.value = m_AmountOfWater;
        }
        else
        {
            m_WaterSliderObj.SetActive(false);
            m_RedWaterSliderObj.SetActive(false);
        }
    }

    //水の量取得
    public int GetAmountOfWater()
    {
        return m_AmountOfWater;
    }

    //水を減らす
    public void DownAmountOfWater(int cost)
    {
        m_AmountOfWater -= cost;
    }

    //水のリロード
    public void WaterReload()
    {
        m_AmountOfWater = AMOUNT_OF_WATER;
        m_RedWaterSlider.value = AMOUNT_OF_WATER;
    }
}
    
