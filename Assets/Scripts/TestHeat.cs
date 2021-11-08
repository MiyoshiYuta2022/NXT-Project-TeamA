using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHeat : MonoBehaviour
{
    //ëÃóÕÅiëÃâ∑Åj
    int m_TestHeat = 100;

    bool m_IsArive = true;
    bool m_TestDownFrag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsArive)
        {
            if (m_TestHeat <= 0)
            {
                m_IsArive = false;
            }
        }
        else if(!m_TestDownFrag)
        {
            this.gameObject.transform.Rotate(90, 0, 0);
            m_TestDownFrag = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //êÖÇ…ìñÇΩÇ¡ÇΩÇÁ
        if (other.gameObject.tag == "Water")
        {
            GameObject water = GameObject.Find("Water(Clone)");
            m_TestHeat -= water.GetComponent<WaterMove>().GetWaterPower();
         
        }
    }
}
