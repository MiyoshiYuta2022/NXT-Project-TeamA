using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;
public class PlayerController : MonoBehaviourPunCallbacks
{
    public float speed = 3f;
    public float jumpPower = 3f;

    private Rigidbody rb;
    private float horizontal, vertical;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody���擾���C��]���Ȃ��悤�ɌŒ�
        rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            // �J�����̉�]�p�̎擾
            float cameraRotateX = Camera.main.transform.localEulerAngles.x;

            //�E�r�̃I�u�W�F�N�g�擾
            GameObject rightarm = transform.Find("RightArm").gameObject;

            // �J�����̉�]�p�ƉE�r�̉�]�p�𓯊�
            rightarm.transform.localEulerAngles = new Vector3(cameraRotateX * -1 + 90, -90.0f, -90.0f);


            // �J�����̉�]�p�̎擾
            float cameraRotateY = Camera.main.transform.localEulerAngles.y;

            // �J�����̉�]�p�ƃv���C���[�̉�]�p�𓯊�
            this.transform.localEulerAngles = new Vector3(0.0f, cameraRotateY - 90.0f, 0.0f);

            //�ړ�����
            if (horizontal != 0 || vertical != 0)
            {
                moveDirection = speed * new Vector3(vertical, 0, -horizontal);
                moveDirection = transform.TransformDirection(moveDirection);
                //rb.velocity = moveDirection;
                rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
            }

            if (isGround == true)//���n���Ă���Ƃ�
            {
                if (Input.GetKeyDown("space"))
                {
                    isGround = false;//  isGround��false�ɂ���
                    rb.AddForce(new Vector3(0, jumpPower, 0)); //��Ɍ������ė͂�������
                }
            }
        }
    }

    void OnCollisionEnter(Collision other) //�n�ʂɐG�ꂽ���̏���
    {
        if (other.gameObject.tag == "Ground") //Ground�^�O�̃I�u�W�F�N�g�ɐG�ꂽ�Ƃ�
        {
            isGround = true; //isGround��true�ɂ���
        }
    }
}
