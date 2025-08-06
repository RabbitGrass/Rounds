using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpPower;

    private bool jumpAble = false;

    public Transform groundCheckTransform;
    public LayerMask groundCheckLayer;
    private bool grounded;

    public Transform wallCheckRight;
    public Transform wallCheckLeft;
    private bool isWall;

    private void Update()
    {
        Debug.Log("벽 : " + isWall);
        Debug.Log("점프 : " + jumpAble);
        if (jumpAble && Input.GetKeyDown(KeyCode.Space)) //GetKeyDown은 반드시 Update에서 써야한다.
        {
            if (isWall && !grounded)
            {
                //벽 점프: x는 벽 반대 방향, y는 위
                float wallJumpDirection = transform.localScale.x > 0 ? -1 : 1;
                Vector2 jumpDir = new Vector2(wallJumpDirection, 1).normalized;

                //rb.velocity = Vector2.zero;//기존 속도 초기화
                rb.AddForce(jumpDir * jumpPower, ForceMode2D.Impulse);
            }
            else if (grounded)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            }
        }
    }

    private void FixedUpdate() //fixedUpdate는 주로 물리엔진과 연관이 된 애들만 넣는다.
    {
        float h = Input.GetAxis("Horizontal");

        Vector2 vector = rb.velocity;

        //transform.Translate(vector * speed * Time.deltaTime);

        vector.x = h * speed;
        rb.velocity = vector;
        UpdateJumpAbleStatus();
    }

    private void UpdateJumpAbleStatus()
    {
        grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.001f, groundCheckLayer);
        isWall = Physics2D.OverlapCircle(wallCheckRight.position, 0.01f, groundCheckLayer) || Physics2D.OverlapCircle(wallCheckRight.position, 0.01f, groundCheckLayer);
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
