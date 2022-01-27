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
            if (playerNum % 2 == 0)
                playerColor = CGameColors.getDefColor(1);
            else
                playerColor = CGameColors.getDefColor(0);
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
            if (_playerNum % 2 == 0)
                playerColor = CGameColors.getDefColor(1);
            else
                playerColor = CGameColors.getDefColor(0);
        }
        return playerColor;
    }
}
