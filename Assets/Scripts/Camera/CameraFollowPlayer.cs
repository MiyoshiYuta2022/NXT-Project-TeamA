using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
            if(player != null)
            {
                _virtualCamera.Follow = player.transform;
            }
        }
    }
}
