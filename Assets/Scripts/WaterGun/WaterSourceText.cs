using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class WaterSourceText : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject m_RelodeText;

    // Start is called before the first frame update
    void Start()
    {
        m_RelodeText.SetActive(false);
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
                m_RelodeText.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.tag == "WaterSource")
            {
                m_RelodeText.SetActive(false);
            }
        }
    }    
}
