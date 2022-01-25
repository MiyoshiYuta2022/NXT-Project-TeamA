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

    public Animator animator;

    private float animWaitTimer;

    private bool b_throwFlag;
    // Start is called before the first frame update
    void Start()
    {
        bombCount = GetComponentInParent<PlayerManager>().bombCount;
        bombCountText.text = "x " + bombCount.ToString();

        animWaitTimer = 0.0f;
        b_throwFlag = false;
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
                if (Input.GetMouseButtonDown(1))
                {
                    b_throwFlag = true;
                    animator.Play("Blue_Throwing");
                }

                if (b_throwFlag == true)
                {
                    animWaitTimer += Time.deltaTime;
                    if(animWaitTimer >= 0.4f)
                    {
                        photonView.RPC(nameof(PlantBomb), RpcTarget.All);
                        b_throwFlag = false;
                        animWaitTimer = 0.0f;
                    }
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
        bombCountText.text = "x " + bombCount.ToString();
    }
}
