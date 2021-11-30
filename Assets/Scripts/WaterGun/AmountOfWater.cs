using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class AmountOfWater : MonoBehaviourPunCallbacks
{
    //水の量
    int m_AmountOfWater;

    //水の量最大
    public const int AMOUNT_OF_WATER = 100;

    // Start is called before the first frame update
    void Start()
    {
        //最初の水の量を設定
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
    }
}
    
