using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpPower;

    private bool jumpAble = false;
    Vector2 direction;

    public Transform groundCheckTransform;
    public LayerMask groundCheckLayer;
    private bool grounded;

    public Transform wallCheckTransform;
    private bool isWall;

    private void Update()
    {
        if (jumpAble && Input.GetKeyDown(KeyCode.Space)) //GetKeyDown은 반드시 Update에서 써야한다.
        {
            if (isWall)
            {
                //vector.y += jumpPower;
                //rb.AddForce(Vector2.);
            }
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate() //fixedUpdate는 주로 물리엔진과 연관이 된 애들만 넣는다.
    {
        float h = Input.GetAxis("Horizontal");
        Debug.Log(jumpAble);

        Vector2 vector = rb.velocity;

        //transform.Translate(vector * speed * Time.deltaTime);

        vector.x = h * speed;
        rb.velocity = vector;
        UpdateJumpAbleStatus();
    }

    private void UpdateJumpAbleStatus()
    {
        grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.001f, groundCheckLayer);
        isWall = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayer);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //if (!grounded)
    //    //{
    //    //    direction = collision.gameObject.transform.position - transform.position;
    //    //    direction = direction.normalized * (-jumpPower);
    //    //}
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((isWall || grounded) && !jumpAble)
        {
            jumpAble = true;
        }
        if (isWall)
        {
            //rb.gravityScale = 0.5f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        jumpAble = false;
        rb.gravityScale = 1;
    }
}
