using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class WaterGunShot : MonoBehaviourPunCallbacks
{
    //水オブジェクト
    public GameObject Water;

    //発射間隔
    public float INTERVAL = 5.5f;

    //発射間隔待ち時間
    float m_FireInterval = 0;

    //方向
    const float DIRECTION_1 = -3;
    const float DIRECTION_2 = 5;

    //方向のカウント
    float m_DirectionCount = 0;

    //水の威力
    int m_WaterPower = 5;

    //消費する水の量
    const int WATER_COST = 2;

    //エフェクト
    public GameObject m_Effect;

    // Start is called before the first frame update
    void Start()
    {
        m_Effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {        
    }
    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            //発射間隔待ち時間が0を下回ったら
            if (m_FireInterval <= 0)
            {
                //マウスを左クリックしている間 または　RBを押している間
                if (Input.GetMouseButton(0) || Input.GetKey("joystick button 5"))
                {
                    //水の量を取得
                    AmountOfWater amountOfWater = gameObject.GetComponent<AmountOfWater>();
                    int check = amountOfWater.GetAmountOfWater();

                    if (check > 0)
                    {
                        //撃つ
                        photonView.RPC(nameof(GunShot), RpcTarget.All);

                        //エフェクト表示
                        m_Effect.SetActive(true);

                        //水を減らす
                        amountOfWater.DownAmountOfWater(WATER_COST);
                    }
                    else
                    {
                        //エフェクト非表示
                        m_Effect.SetActive(false);
                        Debug.Log("Norn Water");
                    }
                }
                else
                {
                    //エフェクト非表示
                    m_Effect.SetActive(false);
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
    private void GunShot()
    {
        //座標
        Vector3 pos = this.gameObject.transform.position;
        //自分のオブジェクト
        GameObject myobject = this.gameObject;
        //回転角度
        Quaternion keeprotation = this.gameObject.transform.rotation;

        //発射パターン
        if (m_DirectionCount == 10)
        {
            myobject.transform.Rotate(0, DIRECTION_1, 0);
            m_DirectionCount = 0;
        }
        else if (m_DirectionCount == 6 || m_DirectionCount == 8)
        {
            myobject.transform.Rotate(0, DIRECTION_2, 0);
            m_DirectionCount++;
        }
        else
        {
            myobject.transform.Rotate(0, 0, 0);
            m_DirectionCount++;
        }


        // Cubeプレハブを元に、インスタンスを生成、
        GameObject waterobject = Instantiate(Water, new Vector3(pos.x, pos.y, pos.z), myobject.transform.rotation);

        //IDをわたす　
        waterobject.GetComponent<WaterMove>().Init(photonView.OwnerActorNr);

        //水の威力を指定
        waterobject.GetComponent<WaterMove>().SetWaterPower(m_WaterPower);

        //インターバルセット
        m_FireInterval = INTERVAL * Time.deltaTime;

        //回転角を戻す
        this.gameObject.transform.rotation = keeprotation;
    }
}
