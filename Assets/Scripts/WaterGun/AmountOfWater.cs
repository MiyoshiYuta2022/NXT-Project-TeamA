using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class AmountOfWater : MonoBehaviourPunCallbacks
{
    //…‚Ì—Ê
    int m_AmountOfWater;

    //…‚Ì—ÊÅ‘å
    public const int AMOUNT_OF_WATER = 100;

    // Start is called before the first frame update
    void Start()
    {
        //Å‰‚Ì…‚Ì—Ê‚ğİ’è
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

    //…‚Ì—Êæ“¾
    public int GetAmountOfWater()
    {
        return m_AmountOfWater;
    }

    //…‚ğŒ¸‚ç‚·
    public void DownAmountOfWater(int cost)
    {
        m_AmountOfWater -= cost;
    }

    //…‚ÌƒŠƒ[ƒh
    public void WaterReload()
    {
        m_AmountOfWater = AMOUNT_OF_WATER;
    }
}
    
