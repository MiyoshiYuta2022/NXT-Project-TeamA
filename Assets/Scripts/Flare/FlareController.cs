using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareController : MonoBehaviour
{
    GameObject m_Player;
    [SerializeField] GameObject m_Flare1;
    [SerializeField] GameObject m_Flare2;
    [SerializeField] GameObject m_Flare3;

    [SerializeField] GameObject m_Light;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // human‚ðHierarchy‚©‚ç‚Ý‚Â‚¯‚é
        m_Player = GameObject.Find("human()(Clone)");
        this.transform.position = m_Light.transform.position - m_Player.transform.position;
        m_Flare1.transform.position = (m_Light.transform.position - m_Player.transform.position) * 1.5f;
        m_Flare2.transform.position = (m_Light.transform.position - m_Player.transform.position);
        m_Flare3.transform.position = (m_Light.transform.position - m_Player.transform.position) / 2;


        this.transform.rotation = m_Flare1.transform.rotation = m_Flare2.transform.rotation = 
            m_Flare3.transform.rotation = m_Light.transform.rotation;
    }
}
