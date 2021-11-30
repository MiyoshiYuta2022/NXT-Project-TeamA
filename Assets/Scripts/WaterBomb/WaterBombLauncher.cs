using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class WaterBombLauncher : MonoBehaviourPunCallbacks
{
    const float INTERVAL = 200;

    //発射間隔待ち時間
    float m_FireInterval = 0;
   
    //水の威力
    int m_WaterPower = 10;

    public GameObject waterBomb;

    public int bombCount;

    public TMP_Text bombCountText;


    // Start is called before the first frame update
    void Start()
    {
        bombCount = GetComponentInParent<PlayerManager>().bombCount;
        bombCountText.text = "Bomb Count: " + bombCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            //発射間隔待ち時間が0を下回ったら
            if (bombCount > 0 && m_FireInterval <= 0)
            {
                //マウスを左クリックしている間
                if (Input.GetMouseButton(1))
                {
                    photonView.RPC(nameof(PlantBomb), RpcTarget.All);
                }
            }
            else
            {
                //インターバルを減らす
                m_FireInterval -= Time.deltaTime;
            }
        }
    }

    [PunRPC]
    private void PlantBomb()
    {
        //座標
        Vector3 pos = this.gameObject.transform.position;
        //自分のオブジェクト
        GameObject myobject = this.gameObject;

        // Cubeプレハブを元に、インスタンスを生成、
        GameObject bomb = Instantiate(waterBomb, new Vector3(pos.x, pos.y, pos.z), myobject.transform.rotation);

        //IDをわたす　
        bomb.GetComponent<WaterBomb>().Init(photonView.OwnerActorNr);

        //水の威力を指定
        bomb.GetComponent<WaterBomb>().SetWaterPower(m_WaterPower);

        m_FireInterval = INTERVAL * Time.deltaTime;

        //Update bomb count in UI
        bombCount--;
        bombCountText.text = "Bomb Count: " + bombCount.ToString();
    }
}
