using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDelete : MonoBehaviour
{
    float m_Scale;
    // Start is called before the first frame update
    void Start()
    {
        m_Scale = 1;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "ColdWater")
        {
            m_Scale -= 0.005f;

            this.transform.localScale =  new Vector3(m_Scale, m_Scale, m_Scale);

            if(m_Scale <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
