using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;
public class PlayerController : MonoBehaviourPunCallbacks
{
    private TestHeat.PLAYER_STATE playerState = TestHeat.PLAYER_STATE.ARIVE;
    private bool b_isGameFinish;

    private GameObject SettingUIManagerObj;
    private SettingUIManager SettingUIManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        SettingUIManagerObj = GameObject.Find("SettingUIManager");
        SettingUIManagerScript = SettingUIManagerObj.GetComponent<SettingUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (b_isGameFinish == true)
        {
            GetComponent<ShowResult>().ShowPanel();
        }
    }

    public void SetPlayerState(TestHeat.PLAYER_STATE nowState)
    {
        playerState = nowState;
    }

    public void SetIsGameFinish(bool isGameFinish)
    {
        b_isGameFinish = isGameFinish;
    }
}    


