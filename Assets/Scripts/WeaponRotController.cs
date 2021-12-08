using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Photon.Pun;

public class WeaponRotController : MonoBehaviourPunCallbacks
{
    [SerializeField] FirstPersonController fps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            this.gameObject.transform.rotation = fps.GetCameraRot();
        }
    }
}
