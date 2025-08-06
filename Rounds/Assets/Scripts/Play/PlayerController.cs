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
        Debug.Log("�� : " + isWall);
        Debug.Log("���� : " + jumpAble);
        if (jumpAble && Input.GetKeyDown(KeyCode.Space)) //GetKeyDown�� �ݵ�� Update���� ����Ѵ�.
        {
            if (isWall && !grounded)
            {
                //�� ����: x�� �� �ݴ� ����, y�� ��
                float wallJumpDirection = transform.localScale.x > 0 ? -1 : 1;
                Vector2 jumpDir = new Vector2(wallJumpDirection, 1).normalized;

                //rb.velocity = Vector2.zero;//���� �ӵ� �ʱ�ȭ
                rb.AddForce(jumpDir * jumpPower, ForceMode2D.Impulse);
            }
            else if (grounded)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            }
        }
    }

    private void FixedUpdate() //fixedUpdate�� �ַ� ���������� ������ �� �ֵ鸸 �ִ´�.
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
