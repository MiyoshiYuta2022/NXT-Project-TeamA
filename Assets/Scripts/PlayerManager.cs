using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gameMode;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
// For player data, respawn/death, receiving other players info
// Attached on game object that is instantiated when player enters game scene - done

public class PlayerManager : MonoBehaviour
{
    private PhotonView photonView;
    private Color playerColor;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // ViewIDs are usually 1001, 2001, etc so this will return a int from 0 to 7
        int playerNum = photonView.ViewID / 1000 - 1;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
