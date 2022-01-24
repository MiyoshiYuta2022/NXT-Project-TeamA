using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Username : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    [SerializeField] TMP_Text nameText;
    private Color nameColor;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponentInParent<PhotonView>();
        if (photonView.IsMine)
            gameObject.SetActive(false);
        nameText.text = photonView.Owner.NickName;
        nameColor = GetComponentInParent<PlayerColor>().playerColor;
        nameText.color = nameColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
