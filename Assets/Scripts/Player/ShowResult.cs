using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResult : MonoBehaviour
{
    private GameObject WinPanel;
    private GameObject LosePanel;
    // Start is called before the first frame update
    void Start()
    {
        WinPanel = GameObject.Find("WinPanel").gameObject;
        LosePanel = GameObject.Find("LosePanel").gameObject;

        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPanel()
    {
        if (GetComponent<TestHeat>().m_PlayerState != TestHeat.PLAYER_STATE.ARIVE)
        {
            LosePanel.SetActive(true);
            Debug.Log(GetComponent<TestHeat>().m_PlayerState);
        }
        else WinPanel.SetActive(true);
    }
}
