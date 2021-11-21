using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;
public class PlayerController : MonoBehaviourPunCallbacks
{
    public float speed = 3f;
    public float jumpPower = 3f;
    public Vector3 localGravity = new Vector3(0.0f, -9.8f, 0.0f);

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

        rb.useGravity = false;
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

            if (isGround == true)//���n���Ă���Ƃ�
            {
                if (Input.GetAxis("Jump") != 0.0f)
                {
                    isGround = false;//  isGround��false�ɂ���
                    rb.AddForce(new Vector3(0, jumpPower, 0)); //��Ɍ������ė͂�������
                }
            }
        }
    }

    private void FixedUpdate()
    {
        SetLocalGravity();

        //�ړ�����
        if (horizontal != 0 || vertical != 0)
        {
            moveDirection = speed * new Vector3(vertical, 0, -horizontal);
            moveDirection = transform.TransformDirection(moveDirection);
            //rb.velocity = moveDirection;
            rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
        }
        else if(isGround == true)
        {
            StopMove();
        }
    }

    void OnCollisionEnter(Collision other) //�n�ʂɐG�ꂽ���̏���
    {
        if (other.gameObject.tag == "Ground") //Ground�^�O�̃I�u�W�F�N�g�ɐG�ꂽ�Ƃ�
        {
            isGround = true; //isGround��true�ɂ���
        }
    }

    void SetLocalGravity()
    {
        rb.AddForce(localGravity, ForceMode.Acceleration);
    }

    void StopMove()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void OnJump()
    {
        if (isGround==true)
        {
            isGround = false;
            rb.AddForce(new Vector3(0, jumpPower, 0));
        }
    }
}
