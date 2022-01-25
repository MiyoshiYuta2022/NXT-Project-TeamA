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
   
    //���̈З�
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
            //���ˊԊu�҂����Ԃ�0�����������
            if (bombCount > 0 && m_FireInterval <= 0)
            {
                //�}�E�X�����N���b�N���Ă����
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

        // Cube�v���n�u�����ɁA�C���X�^���X�𐶐��A
        GameObject bomb = Instantiate(waterBomb, new Vector3(pos.x, pos.y, pos.z), myobject.transform.rotation);

        //ID���킽���@
        bomb.GetComponent<WaterBomb>().Init(photonView.OwnerActorNr);

        //���̈З͂��w��
        bomb.GetComponent<WaterBomb>().SetWaterPower(m_WaterPower);

        m_FireInterval = INTERVAL * Time.deltaTime;

        //Update bomb count in UI
        bombCount--;
        bombCountText.text = "x " + bombCount.ToString();
    }
}
