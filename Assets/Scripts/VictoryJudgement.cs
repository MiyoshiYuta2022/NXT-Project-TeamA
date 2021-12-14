using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Characters.FirstPerson;

public class VictoryJudgement : MonoBehaviourPunCallbacks
{
    private int playerNum;
    private bool b_isGameFinish;

    private bool isSoloPlaying;

    private GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        playerNum = PhotonNetwork.PlayerList.Length;
        Debug.Log("NowPlayerNum = " + playerNum);
        b_isGameFinish = false;
        // テスト用のプレイ時に勝利判定を行いわないためのフラグ
        if (playerNum == 1) isSoloPlaying = true;
        else isSoloPlaying = false;

        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // テストプレイでなければ
        if(isSoloPlaying == false)
        {
            // 残りの生存プレイヤーが1人であれば
            if(playerNum == 1)
            {
                // 試合が終了している
                b_isGameFinish = true;
                playerObj.GetComponent<PlayerController>().SetIsGameFinish(b_isGameFinish);
                playerObj.GetComponent<FirstPersonController>().SetIsGameFinish(b_isGameFinish);
                GameObject.Find("Launch port").GetComponent<WaterGunShot>().SetIsGameFinish(b_isGameFinish);
            }
        }
    }

    public void IsPlayerDead()
    {
        photonView.RPC(nameof(IsPlayerDeadRPC), RpcTarget.All);
       
    }

    [PunRPC]
    private void IsPlayerDeadRPC()
    {
        playerNum--;
        Debug.Log("NowPlayerNum = " + playerNum);
    }

    public bool GetIsGameFinish()
    {
        return b_isGameFinish;
    }
}
