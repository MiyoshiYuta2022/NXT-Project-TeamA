using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class WaterSourceText : MonoBehaviourPunCallbacks
{
    GameObject m_RelodeText;

    // Start is called before the first frame update
    void Start()
    {
        m_RelodeText = GameObject.Find("RelodeText");
            m_RelodeText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.tag == "WaterSource" || other.tag == "ColdWater")
            {
                m_RelodeText.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.tag == "WaterSource" || other.tag == "ColdWater")
            {
                m_RelodeText.SetActive(false);
            }
        }
    }    
}
