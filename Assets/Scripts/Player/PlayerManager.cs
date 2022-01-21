using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gameMode;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using TMPro;
// For player data, respawn/death, receiving other players info
// Attached on game object that is instantiated when player enters game scene - done

public class PlayerManager : MonoBehaviour
{
    private PhotonView photonView;
    public Color playerColor;
    public int bombCount;
    public GameObject playerUI;
    private float survivalTime;
    private bool bFreeze;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
        int playerNum = photonView.OwnerActorNr - 1;

        // returns gameMode selected from player properties (master client as others are null)
        bool gameMode = (bool)PhotonNetwork.MasterClient.CustomProperties["Mode"];
        //true - single mode, false - team mode
        if (gameMode)
        {
            playerColor = CGameColors.getDefColor(playerNum);
        }
        else
        {
            // every 2 players form 1 team
            if (playerNum < 2)
                playerColor = CGameColors.getDefColor(0);
            else if (playerNum < 4)
                playerColor = CGameColors.getDefColor(1);
            else if (playerNum < 6)
                playerColor = CGameColors.getDefColor(2);
            else if (playerNum < 8)
                playerColor = CGameColors.getDefColor(3);
            else
                playerColor = CGameColors.getDefColor(default);
        }
        bombCount = 10;
        bFreeze = false;

        // display only player UI
        if (photonView.IsMine)
            playerUI.SetActive(true);
        else
            playerUI.SetActive(false);
            

    }

    // for interaction with cold water
    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "ColdWater")
            {
                playerUI.transform.GetChild(0).gameObject.SetActive(true);
                bFreeze = true;
                survivalTime = GetComponent<TestHeat>().m_Hp / 10;
            }
            //else if(other.gameObject.name == "Instantkill")
            //{
            //    Debug.Log("Instant death");
            //    GetComponent<TestHeat>().m_Hp = 0;
            //    GetComponent<TestHeat>().m_PlayerState = TestHeat.PLAYER_STATE.DEATH;
            //    GameObject.Find("VictoryJudgement").GetComponent<VictoryJudgement>().IsPlayerDead();
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ColdWater" && photonView.IsMine)
        {
            playerUI.transform.GetChild(0).gameObject.SetActive(false);
            bFreeze = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //start countdown when interact with cold water
        if (bFreeze) 
        {
            survivalTime -= Time.deltaTime;
            if (survivalTime < 0f)
            {
                if (GetComponent<TestHeat>().m_PlayerState != TestHeat.PLAYER_STATE.DEATH)
                {
                    GetComponent<TestHeat>().m_Hp = 0;
                    GetComponent<TestHeat>().m_PlayerState = TestHeat.PLAYER_STATE.DEATH;
                    GameObject.Find("VictoryJudgement").GetComponent<VictoryJudgement>().IsPlayerDead();

                    return;
                }
            }
            else
            {
                GetComponent<TestHeat>().m_Hp = (int)survivalTime * 10;
            }
        }
    }
}
