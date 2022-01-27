using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockChack : MonoBehaviour
{    
    [SerializeField] GameObject m_FlareParent;

    GameObject m_Player;

    float m_Scale;

    const float FADE_CRITERION = 75;

    // Start is called before the first frame update
    void Start()
    {
        m_Scale = 0;
        m_FlareParent.transform.localScale = new Vector3(m_Scale,m_Scale,m_Scale);
    }

    // Update is called once per frame
    void Update()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        GameObject cameraObj = m_Player.transform.Find("FirstPersonCharacter").gameObject;
        Camera camera = cameraObj.GetComponent<Camera>();
        float y = m_Player.transform.localEulerAngles.y;
        y += 30;
                
        while(y >= 360)
        {
            y -= 360;
        }

        if (y < 180 && y > 180 - FADE_CRITERION)
        {
            y -= 180 - FADE_CRITERION;

            m_Scale = y / FADE_CRITERION;
        }
        else if (y > 180 && y < 180 + FADE_CRITERION)
        {
            y -= 180 + FADE_CRITERION;
            float set = FADE_CRITERION + y;
            m_Scale = 1 - (set / FADE_CRITERION);
        }
        else
        {
            m_Scale = 0;
        }
        m_FlareParent.transform.localScale = new Vector3(m_Scale, m_Scale, m_Scale);
    }
    

    void OnBecameVisible()
    {       
    }
    void OnBecameInvisible()
    {
    }
}
