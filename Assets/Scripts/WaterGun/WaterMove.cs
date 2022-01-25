using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gameMode;
public class WaterMove : MonoBehaviour
{
    //�ړ����x
    const float SPEED = 40f;

    //������܂ł̎���
    const float DESTROY_TIME = 6;

    //�����鎞�Ԃ̃J�E���g
    float m_DestroyCount = 0;

    //�w�肳��Ȃ��������̈З�
    int m_WaterPower = 1;

    //�o�����l��ID
    [SerializeField] int m_OwnerId;

    Color waterColor;

    //ID��ݒ�
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
        //�ړ�
        this.gameObject.transform.Translate(0, 0, SPEED * Time.deltaTime);

        //�����鎞�Ԃ𑝂₷
        m_DestroyCount += Time.deltaTime;

        //���Ԃ𒴂��������
        if (m_DestroyCount > DESTROY_TIME)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //���ɓ���������
        if (other.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        //�v���C���[�ɓ���������
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

    //���̈З͂��擾
    public int GetWaterPower()
    {
        return m_WaterPower;
    }
    //���̈З͂̐ݒ�
    public void SetWaterPower(int waterpower)
    {
        m_WaterPower = waterpower;
    }

    //ID�̎擾
    public int GetOwnerId()
    {
        return m_OwnerId;
    }
}
