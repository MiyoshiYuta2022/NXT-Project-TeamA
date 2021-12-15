using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class WaterGunShot : MonoBehaviourPunCallbacks
{
    //���I�u�W�F�N�g
    [SerializeField] GameObject Water;

    //���ˊԊu
    const float INTERVAL = 5.5f;

    //���ˊԊu�҂�����
    float m_FireInterval = 0;

    //����
    const float DIRECTION_1 = -3;
    const float DIRECTION_2 = 5;

    //�����̃J�E���g
    float m_DirectionCount = 0;

    //���̈З�
    int m_WaterPower = 3;

    //����鐅�̗�
    const int WATER_COST = 2;

    //�G�t�F�N�g
    [SerializeField] GameObject m_Effect;

    //�Q�[�����I���������ǂ���
    private bool b_isGameFinish;

    private GameObject SettingUIManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        m_Effect.SetActive(false);
        b_isGameFinish = false;
        SettingUIManagerObj = GameObject.Find("SettingUIManager");
    }

    // Update is called once per frame
    void Update()
    {        
    }
    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            if (b_isGameFinish == false && SettingUIManagerObj.GetComponent<SettingUIManager>().GetMenuMode() == false)
            {
                //���ˊԊu�҂����Ԃ�0�����������
                if (m_FireInterval <= 0)
                {
                    //�}�E�X�����N���b�N���Ă���� �܂��́@RB�������Ă����
                    if (Input.GetMouseButton(0) || Input.GetKey("joystick button 5"))
                    {
                        //���̗ʂ��擾
                        AmountOfWater amountOfWater = gameObject.GetComponent<AmountOfWater>();
                        int check = amountOfWater.GetAmountOfWater();

                        if (check > 0)
                        {
                            //����
                            photonView.RPC(nameof(GunShot), RpcTarget.All);

                            //�G�t�F�N�g�\��
                            m_Effect.SetActive(true);

                            //�������炷
                            amountOfWater.DownAmountOfWater(WATER_COST);
                        }
                        else
                        {
                            //�G�t�F�N�g��\��
                            m_Effect.SetActive(false);
                            Debug.Log("Norn Water");
                        }
                    }
                    else
                    {
                        //�G�t�F�N�g��\��
                        m_Effect.SetActive(false);
                    }
                }
                else
                {
                    //�C���^�[�o�������炷
                    m_FireInterval -= Time.deltaTime;
                }
            }
        }
    }

    [PunRPC]
    private void GunShot()
    {
        //���W
        Vector3 pos = this.gameObject.transform.position;
        //�����̃I�u�W�F�N�g
        GameObject myobject = this.gameObject;
        //��]�p�x
        Quaternion keeprotation = this.gameObject.transform.rotation;

        //���˃p�^�[��
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


        // Cube�v���n�u�����ɁA�C���X�^���X�𐶐��A
        GameObject waterobject = Instantiate(Water, new Vector3(pos.x, pos.y, pos.z), myobject.transform.rotation);

        //ID���킽���@
        waterobject.GetComponent<WaterMove>().Init(photonView.OwnerActorNr);

        //���̈З͂��w��
        waterobject.GetComponent<WaterMove>().SetWaterPower(m_WaterPower);

        //�C���^�[�o���Z�b�g
        m_FireInterval = INTERVAL * Time.deltaTime;

        //��]�p��߂�
        this.gameObject.transform.rotation = keeprotation;
    }

    public void SetIsGameFinish(bool isGameFinish)
    {
        b_isGameFinish = isGameFinish;
    }
}
