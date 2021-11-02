using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float jumpPower = 3f;

    private Rigidbody rb;
    private float horizontal, vertical;
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //RigidbodyÇéÊìæÇµÅCâÒì]ÇµÇ»Ç¢ÇÊÇ§Ç…å≈íË
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            moveDirection = speed * new Vector3(vertical, 0, -horizontal);
            moveDirection = transform.TransformDirection(moveDirection);
            rb.velocity = moveDirection;
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
    }
}
