using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class WaterBombLauncher : MonoBehaviourPunCallbacks
{
    const float INTERVAL = 200;

    //���ˊԊu�҂�����
    float m_FireInterval = 0;

    //����
    const float DIRECTION_1 = -3;
    const float DIRECTION_2 = 5;

    //�����̃J�E���g
    float m_DirectionCount = 0;

    //���̈З�
    int m_WaterPower = 2;

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
            //���ˊԊu�҂����Ԃ�0�����������
            if (bombCount > 0 && m_FireInterval <= 0)
            {
                //�}�E�X�����N���b�N���Ă����
                if (Input.GetMouseButton(1))
                {
                    photonView.RPC(nameof(PlantBomb), RpcTarget.All);
                }
            }
            else
            {
                //�C���^�[�o�������炷
                m_FireInterval -= Time.deltaTime;
            }

        }
    }

    [PunRPC]
    private void PlantBomb()
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
        GameObject bomb = Instantiate(waterBomb, new Vector3(pos.x, pos.y, pos.z), myobject.transform.rotation);

        //ID���킽���@
        bomb.GetComponent<WaterBomb>().Init(photonView.OwnerActorNr);

        //���̈З͂��w��
        bomb.GetComponent<WaterBomb>().SetWaterPower(m_WaterPower);

        m_FireInterval = INTERVAL * Time.deltaTime;

        //��]�p��߂�
        this.gameObject.transform.rotation = keeprotation;

        //Update bomb count in UI
        bombCount--;
        bombCountText.text = "Bomb Count: " + bombCount.ToString();
    }
}