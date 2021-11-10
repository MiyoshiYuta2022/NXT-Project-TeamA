using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;


// player/team color
namespace gameColor
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

        public static Color getDefColor(GameColors c)
        {
            switch (c)
            {
                case GameColors.GC_RED:
                    return Color.red;
                case GameColors.GC_BLUE:
                    return Color.blue;
                case GameColors.GC_GREEN:
                    return Color.green;
                case GameColors.GC_YELLOW:
                    return Color.yellow;
                case GameColors.GC_MAGENTA:
                    return Color.magenta;
                case GameColors.GC_CYAN:
                    return Color.cyan;
                case GameColors.GC_BLACK:
                    return Color.black;
                case GameColors.GC_WHITE:
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
