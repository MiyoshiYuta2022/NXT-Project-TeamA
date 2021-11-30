using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class WaterSourceText : MonoBehaviourPunCallbacks
{
    public GameObject m_Text;

    // Start is called before the first frame update
    void Start()
    {
        m_Text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.tag == "WaterSource")
            {
                m_Text.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.tag == "WaterSource")
            {
                m_Text.SetActive(false);
            }
        }
    }    
}
