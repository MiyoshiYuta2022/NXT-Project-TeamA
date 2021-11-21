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
        //Rigidbodyを取得し，回転しないように固定
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

            // カメラの回転角の取得
            float cameraRotateX = Camera.main.transform.localEulerAngles.x;

            //右腕のオブジェクト取得
            GameObject rightarm = transform.Find("RightArm").gameObject;

            // カメラの回転角と右腕の回転角を同期
            rightarm.transform.localEulerAngles = new Vector3(cameraRotateX * -1 + 90, -90.0f, -90.0f);

            // カメラの回転角の取得
            float cameraRotateY = Camera.main.transform.localEulerAngles.y;

            // カメラの回転角とプレイヤーの回転角を同期
            this.transform.localEulerAngles = new Vector3(0.0f, cameraRotateY - 90.0f, 0.0f);

            if (isGround == true)//着地しているとき
            {
                if (Input.GetAxis("Jump") != 0.0f)
                {
                    isGround = false;//  isGroundをfalseにする
                    rb.AddForce(new Vector3(0, jumpPower, 0)); //上に向かって力を加える
                }
            }
        }
    }

    private void FixedUpdate()
    {
        SetLocalGravity();

        //移動処理
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

    void OnCollisionEnter(Collision other) //地面に触れた時の処理
    {
        if (other.gameObject.tag == "Ground") //Groundタグのオブジェクトに触れたとき
        {
            isGround = true; //isGroundをtrueにする
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
