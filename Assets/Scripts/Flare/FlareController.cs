using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlareController : MonoBehaviour
{
    GameObject m_Player;
    [SerializeField] GameObject m_FlareParent;
    [SerializeField] GameObject m_Flare1;
    [SerializeField] GameObject m_Flare2;
    [SerializeField] GameObject m_Flare3;

    [SerializeField] Transform m_Target;

    Rect rect = new Rect(0, 0, 1, 1); // ‰æ–Ê“à‚©”»’è‚·‚é‚½‚ß‚ÌRect

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (m_FlareParent.activeSelf)
        {
            // human‚ðHierarchy‚©‚ç‚Ý‚Â‚¯‚é
            m_Player = GameObject.Find("human()(Clone)");
           // GameObject cameraObj =  m_Player.transform.Find("FirstPersonCharacter").gameObject;
          //  Camera camera = cameraObj.GetComponent<Camera>();

            this.transform.position = m_Target.transform.position - m_Player.transform.position;
            m_Flare1.transform.position = (m_Target.transform.position - m_Player.transform.position) / 3;
            m_Flare2.transform.position = (m_Target.transform.position - m_Player.transform.position) / 10;
            m_Flare3.transform.position = (m_Target.transform.position - m_Player.transform.position) / 30;
        }
    }

}