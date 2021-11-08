using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    private TMP_Text text;
    private Photon.Realtime.Player player;
    
    public void setUp(Photon.Realtime.Player _player)
    {
        text = GetComponentInChildren<TMP_Text>();
        player = _player;
        text.text = _player.NickName;
        if(PhotonNetwork.LocalPlayer==_player)
        {
            text.color = Color.red;
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }


}
