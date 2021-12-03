using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{
    private TMP_Text text;
    public RoomInfo info;

    public void setUp(RoomInfo _info)
    {
        text = GetComponentInChildren<TMP_Text>();
        info = _info;
        text.text = _info.Name + " --- " + _info.PlayerCount + " / " + _info.MaxPlayers;
    }

    public void onClick()
    {
        LobbyManager.instance.joinRoom(info);
    }

}
