using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

// for multiplayer lobby code (creating/finding room, connection to server, etc) and UI toggle
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instance;

    [SerializeField] GameObject loadingPanel;
    [SerializeField] GameObject playPanel;    
    [SerializeField] GameObject createRoomPanel;
    [SerializeField] GameObject findRoomPanel;
    [SerializeField] GameObject lobbyPanel;
    [SerializeField] TMP_InputField roomNameInput;
    [SerializeField] TMP_Text createRoomErrorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;    
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] GameObject cancelButton;

    private void Awake()
    {
        instance = this;
    }

    // connect to Photon server when exe launches
    void Start()
    {
        Debug.Log("Connecting to server");
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
        loadingPanel.SetActive(true);
        cancelButton.SetActive(false);
    }

    // join lobby once connected to server
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        cancelButton.SetActive(true);
    }

    // toggle menu UI and set random player nickname when in lobby
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to lobby");
        loadingPanel.SetActive(false);
        playPanel.SetActive(true);

        //set nickname for player
        if(PlayerPrefs.HasKey("username"))
        {
            usernameInput.text = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = PlayerPrefs.GetString("username");
        }
        else
        {
            PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
            changeUsername();
        }
    }

    // debug log if disconnected from server
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for: " + cause.ToString());
    }

    // toggle from menu to create/find room UI
    public void changeRoomPanel(GameObject roomPanel)
    {
        playPanel.SetActive(false);
        roomPanel.SetActive(true);
    }

    // toggle from create/find room to menu UI
    public void changeMenu(GameObject roomPanel)
    {
        playPanel.SetActive(true);
        roomPanel.SetActive(false);
    }

    // return to main menu if stuck in loading screen
    public void cancelAction()
    {
        playPanel.SetActive(true);
        loadingPanel.SetActive(false);
    }

    // create new room
    public void createRoom()
    {
        // display error message if room name is invalid
        if (string.IsNullOrEmpty(roomNameInput.text))
        {
            createRoomErrorText.text = "Invalid room name";
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInput.text);
        loadingPanel.SetActive(true);
        createRoomPanel.SetActive(false);
    }

    // settings when join room
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        loadingPanel.SetActive(false);
        lobbyPanel.SetActive(true);

        // display room name
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        // get player list
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        //display list of player in the room
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < players.Length; ++i)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().setUp(players[i]);
        }

        // display start game button only for room leader
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    // display start game button for new room leader is changed
    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    // error message if room creation failed
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Create Room Failed - Return Code: " + returnCode + " Message: " + message);
        createRoomErrorText.text ="Room creation failed:" +  message;
    }

    // toggle of UI when leaving room
    public void LeaveRoom()
    {
        Debug.Log("Leaving room");
        PhotonNetwork.LeaveRoom();
        loadingPanel.SetActive(true);
        lobbyPanel.SetActive(false);
    }

    // toggle of UI when left room
    public override void OnLeftRoom()
    {
        Debug.Log("Left room");
        loadingPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    // disconnect from server and exit exe when quit button is pressed
    public void quitGame()
    {
        Debug.Log("Disconnecting server");
        PhotonNetwork.Disconnect();
        Application.Quit();
        if(!PhotonNetwork.IsConnected)
        {
            Debug.Log("Disconnected from server");
        }    
    }

    // update room list in find room UI
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trfm in roomListContent)
        {
            Destroy(trfm.gameObject);
        }
        for (int i = 0; i < roomList.Count; ++i)
        {
            if(roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().setUp(roomList[i]);
        }
    }

    // toggle of UI when joining room
    public void joinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        findRoomPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }

    // display player name is player list
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().setUp(newPlayer);
    }

    // change of scene when start game is pressed
    public void startGame()
    {
        // based on order in build settings (game scene is at index 1)
        PhotonNetwork.LoadLevel(1);
    }

    // change username
    public void changeUsername()
    {
        if (string.IsNullOrEmpty(usernameInput.text))
        {
            return;
        }
        PhotonNetwork.NickName = usernameInput.text;
        PlayerPrefs.SetString("username", usernameInput.text);
        Debug.Log("Change name success");
    }

}
