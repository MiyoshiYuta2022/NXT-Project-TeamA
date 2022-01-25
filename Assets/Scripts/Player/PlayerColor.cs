using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using gameMode;
public class PlayerColor : MonoBehaviour
{
    public Color playerColor;
    private PhotonView photonView;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color setColorWater(int _playerNum)
    {
        bool gameMode = (bool)PhotonNetwork.MasterClient.CustomProperties["Mode"];
        //true - single mode, false - team mode
        if (gameMode)
        {
            playerColor = CGameColors.getDefColor(_playerNum);
        }
        else
        {
            // every 2 players form 1 team
            if (_playerNum < 2)
                playerColor = CGameColors.getDefColor(0);
            else if (_playerNum < 4)
                playerColor = CGameColors.getDefColor(1);
            else if (_playerNum < 6)
                playerColor = CGameColors.getDefColor(2);
            else if (_playerNum < 8)
                playerColor = CGameColors.getDefColor(3);
            else
                playerColor = CGameColors.getDefColor(default);
        }
        return playerColor;
    }
}
