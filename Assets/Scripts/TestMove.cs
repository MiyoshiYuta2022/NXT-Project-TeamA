
using UnityEngine;



// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class TestMove : MonoBehaviour
{
    const float SPEED = 10.0f;
    const float RESET_POS_Y = -20;

    const float JUMP_START_SPEED = 0.15f;
    const float JUMP_SPEED_DOWN = 0.002f;
    const float JUMP_SPEED_MIN = -0.05f;

    bool m_Jumpfrag = false;
    float m_JumpSpeed = 0;


    private void Update()
    {   
            //移動処理　テスト
            if (Input.GetKey(KeyCode.A))
            {
                this.gameObject.transform.Translate(-SPEED * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.gameObject.transform.Translate(SPEED * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.Translate(0, 0, SPEED * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.transform.Translate(0, 0, -SPEED * Time.deltaTime);
            }

            //ジャンプテスト
            if (!m_Jumpfrag)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_Jumpfrag = true;
                    m_JumpSpeed = JUMP_START_SPEED;                }
            }
            if (m_Jumpfrag)
            {
                if (m_JumpSpeed > JUMP_SPEED_MIN)
                {
                    m_JumpSpeed -= JUMP_SPEED_DOWN;
                }

                this.gameObject.transform.Translate(0, m_JumpSpeed, 0);
            }

            Vector3 pos = this.gameObject.transform.position;

            //ある座標よりも低かったら戻るテスト
            if (pos.y < RESET_POS_Y)
            {
                pos = new Vector3(0, 0, 0);
                this.gameObject.transform.position = pos;
            }

        
    }

    void OnCollisionStay(Collision other)
    {
        //床に当たったら
        if (other.gameObject.tag == "Floor")
        {
            m_Jumpfrag = false;
            m_JumpSpeed = 0;

        }
    }

}