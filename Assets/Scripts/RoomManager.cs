using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;


// player/team color
namespace gameMode
{
    public class CGameColors
    {
        public enum GameColors
        {
            GC_RED = 0,
            GC_BLUE,
            GC_YELLOW,
            GC_GREEN,
            GC_MAGENTA,
            GC_CYAN,
            GC_BLACK,
            GC_WHITE,
            GC_TOTAL
        }

        public static Color getDefColor(int c)
        {
            switch (c)
            {
                case 0:
                    return Color.red;
                case 1:
                    return Color.blue;
                case 2:
                    return Color.green;
                case 3:
                    return Color.yellow;
                case 4:
                    return Color.magenta;
                case 5:
                    return Color.cyan;
                case 6:
                    return Color.black;
                case 7:
                    return Color.white;
                default:
                    return Color.gray;
            }
        }
    }
}

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;
    private bool gamemode;
    private void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    public void onSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex==1) // in first game/level scene
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerManager"),Vector3.zero,Quaternion.identity);

            //can i move this line to inside the player manager script?/この行をプレーヤーマネージャースクリプト内に移動できますか？
            PhotonNetwork.Instantiate(Path.Combine("human()"), Vector3.zero, Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
