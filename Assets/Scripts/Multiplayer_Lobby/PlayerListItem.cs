using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    private TMP_Text text;
    private Player player;

    public void setUp(Player _player)
    {
        text = GetComponentInChildren<TMP_Text>();
        player = _player;
        text.text = _player.NickName;
        if (PhotonNetwork.LocalPlayer == _player)
        {
            text.color = Color.red;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
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
