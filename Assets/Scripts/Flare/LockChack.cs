using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockChack : MonoBehaviour
{    
    [SerializeField] GameObject m_FlareParent;

    GameObject m_Player;


    float m_Scale;
    bool m_ScaleUpFrag;
    bool m_ScaleDownFrag;
    bool m_ScaleStopFrag;
    bool m_LockFrag;

    // Start is called before the first frame update
    void Start()
    {
        m_Scale = 0;
        m_ScaleUpFrag = false;
        m_ScaleDownFrag = false;
        m_ScaleStopFrag = false;
        m_LockFrag = false;

        m_FlareParent.transform.localScale = new Vector3(m_Scale,m_Scale,m_Scale);
    }

    // Update is called once per frame
    void Update()
    {
        m_Player = GameObject.Find("human()(Clone)");
        GameObject cameraObj = m_Player.transform.Find("FirstPersonCharacter").gameObject;
        Camera camera = cameraObj.GetComponent<Camera>();


        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);

        if (m_LockFrag)
        {
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "Flare")
                {
                    ScaleUp();
                }
                else
                {
                    ScaleDown();
                }
            }
            else
            {
                ScaleUp();
            }
        }
        else
        {
            ScaleDown();
        }

        if (!m_ScaleStopFrag)
        {
            if (m_ScaleUpFrag)
            {               
                if (m_Scale >= 1)
                {
                    m_ScaleUpFrag = false;
                    m_ScaleStopFrag = true;
                }
                else
                {
                    m_Scale += 0.025f;
                }
            }
            else if (m_ScaleDownFrag)
            {               
                if (m_Scale <= 0)
                {
                    m_ScaleDownFrag = false;
                    m_ScaleStopFrag = true;
                    m_FlareParent.SetActive(false);
                }
                else
                {
                    m_Scale -= 0.025f;
                }
            }
            m_FlareParent.transform.localScale = new Vector3(m_Scale, m_Scale, m_Scale);
        }
        

    }
    

    void OnBecameVisible()
    {
        m_LockFrag = true;
    }
    void OnBecameInvisible()
    {
        m_LockFrag = false;
    }

    void ScaleUp()
    {
        m_ScaleUpFrag = true;
        m_ScaleDownFrag = false;
        m_ScaleStopFrag = false;
        m_FlareParent.SetActive(true);
    }

    void ScaleDown()
    {
        m_ScaleUpFrag = false;
        m_ScaleDownFrag = true;
        m_ScaleStopFrag = false;
    }
}
