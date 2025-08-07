using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private float wallJumpSpeed;
    private bool isWallJumping = false;
    public float wallJumpDuration;
    private float wallJumpTimer = 0f;
    public float jumpPower;

    private bool jumpAble = false;

    public Transform groundCheckTransform;
    public LayerMask groundCheckLayer;
    private bool grounded;

    public Transform wallCheckRight;
    public Transform wallCheckLeft;
    private bool isWallRight = false;
    private bool isWallLeft = false;

    private void Start()
    {
        wallJumpSpeed = speed * 0.5f;
    }

    private void Update()
    {
        if (jumpAble && Input.GetKeyDown(KeyCode.Space)) //GetKeyDown은 반드시 Update에서 써야한다.
        {
            float wallJumpDirection = 0;
            if (grounded)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            }
            else if (!grounded)
            {
                if (isWallRight)
                {
                    //벽 점프: x는 벽 반대 방향, y는 위
                    wallJumpDirection = -1;
                }
                else if (isWallLeft)
                {
                    wallJumpDirection = 1;
                }

                //rb.velocity = Vector2.zero;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
                //Vector2 jumpDir = new Vector2(wallJumpDirection, 1).normalized;
                //rb.AddForce(jumpDir * jumpPower, ForceMode2D.Impulse);
                rb.velocity = new Vector2(wallJumpDirection * wallJumpSpeed, jumpPower);
                isWallJumping = true;
                wallJumpTimer = wallJumpDuration;
            }
        }

        if (isWallJumping)
        {
            wallJumpTimer -= Time.deltaTime;
            if (wallJumpTimer <= 0)
                isWallJumping = false;
        }
    }

    private void FixedUpdate() //fixedUpdate는 주로 물리엔진과 연관이 된 애들만 넣는다.
    {
        float h = Input.GetAxis("Horizontal");

        Vector2 vector = rb.velocity;
        vector.x = h * speed;


        if ((!isWallRight && !isWallLeft && !isWallJumping) || grounded)
            rb.velocity = vector;

        UpdateJumpAbleStatus();

        SlideWall();
    }

    private void UpdateJumpAbleStatus()
    {
        grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.001f, groundCheckLayer);
        isWallRight = Physics2D.OverlapCircle(wallCheckRight.position, 0.01f, groundCheckLayer);
        isWallLeft = Physics2D.OverlapCircle(wallCheckLeft.position, 0.01f, groundCheckLayer);

        jumpAble = grounded || isWallLeft || isWallRight;
    }

    private void SlideWall()
    {
        if (!grounded && (isWallLeft || isWallRight))
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -2f, float.MaxValue)); // 느리게 떨어지기
        }
    }

}
