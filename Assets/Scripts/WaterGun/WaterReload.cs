using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class WaterReload : MonoBehaviourPunCallbacks
{
    bool m_HitWaterSourceFrag = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (m_HitWaterSourceFrag)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    //ÉäÉçÅ[Éh
                    GameObject.Find("Launch port").GetComponent<AmountOfWater>().WaterReload();
                    Debug.Log("Reload");
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if(other.tag == "WaterSource")
            {
                m_HitWaterSourceFrag = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.tag == "WaterSource")
            {
                m_HitWaterSourceFrag = false;
            }
        }
    }
}
