using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gameMode;
public class WaterMove : MonoBehaviour
{
    //移動速度
    const float SPEED = 40f;

    //消えるまでの時間
    const float DESTROY_TIME = 6;

    //消える時間のカウント
    float m_DestroyCount = 0;

    //指定されなかった時の威力
    int m_WaterPower = 1;

    //出した人のID
    [SerializeField] int m_OwnerId;

    Color waterColor;

    //IDを設定
    public void Init(int ownerId)
    {
        m_OwnerId = ownerId;
    }

    // Start is called before the first frame update
    void Start()
    {
        waterColor = gameObject.GetComponent<PlayerColor>().setColorWater(m_OwnerId - 1);
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        this.gameObject.transform.Translate(0, 0, SPEED * Time.deltaTime);

        //消える時間を増やす
        m_DestroyCount += Time.deltaTime;

        //時間を超えたら消す
        if (m_DestroyCount > DESTROY_TIME)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //床に当たったら
        if (other.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        //プレイヤーに当たったら
        if (other.gameObject.tag == "PlayerCollision")
        {
            GameObject parent = other.gameObject.transform.root.gameObject;
            //only can damage opponent
            bool bdiffColor = waterColor != parent.GetComponent<PlayerColor>().playerColor;

            if (GetOwnerId() != parent.GetComponent<TestHeat>().GetOwnerId() && bdiffColor)
                parent.GetComponent<TestHeat>().HpDowm(GetWaterPower());

            Destroy(this.gameObject);
        }
    }

    //水の威力を取得
    public int GetWaterPower()
    {
        return m_WaterPower;
    }
    //水の威力の設定
    public void SetWaterPower(int waterpower)
    {
        m_WaterPower = waterpower;
    }

    //IDの取得
    public int GetOwnerId()
    {
        return m_OwnerId;
    }
}
