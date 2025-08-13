using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed; //3.5
    private float wallJumpSpeed;
    private float moveSpeed;

    private bool isWallJumping = false;
    public float wallJumpDuration;
    private float wallJumpTimer = 0f;
    public float jumpPower;
    private float wallJumpPower;

    private bool jumpAble = false; //점프 가능여부

    public Transform groundCheckTransform;//땅 감지
    public LayerMask groundCheckLayer;
    private bool grounded;

    public Transform wallCheckRight;//오른쪽 벽 감지
    public Transform wallCheckLeft; //왼쪽 벽 감지
    private bool isWallRight = false;
    private bool isWallLeft = false;

    public ParticleSystem ShildCharge;
    public ParticleSystem Shild;
    public ParticleSystem Charging;
    public float boundTime;
    private bool isShildCharge = true;
    public bool isShild = false;
    private void Start()
    {
        wallJumpSpeed = jumpPower * 0.5f;
        wallJumpPower = jumpPower - 0.5f;
        moveSpeed = speed;

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
                rb.velocity = new Vector2(wallJumpDirection * wallJumpSpeed, wallJumpPower);
                isWallJumping = true;
                wallJumpTimer = wallJumpDuration;
            }
        }

        if (isWallJumping)
        {
            wallJumpTimer -= Time.deltaTime;
            if (wallJumpTimer <= 0)
            {
                isWallJumping = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && isShildCharge)
        {
            StartCoroutine(ShildActive());
        }

    }

    private void FixedUpdate() //fixedUpdate는 주로 물리엔진과 연관이 된 애들만 넣는다.
    {
        float h = Input.GetAxis("Horizontal");

        Vector2 vector = rb.velocity;
        
        vector.x = h * moveSpeed;


        if(!isWallJumping)
            rb.velocity = vector;

        UpdateJumpAbleStatus();

        //SlideWall();
    }

    IEnumerator ShildActive()
    {
        Debug.Log("실행");
        isShildCharge = false;
        isShild = true;
        Shild.gameObject.SetActive(!isShildCharge);
        Charging.gameObject.SetActive(!isShildCharge);
        ShildCharge.gameObject.SetActive(isShildCharge);
        HpController hp = gameObject.GetComponent<HpController>();
        hp.isShild = isShild;
        yield return new WaitForSeconds(0.3f);
        isShild = false;
        hp.isShild = isShild;
        yield return new WaitForSeconds(boundTime);
        Debug.Log("end");
        isShildCharge = true;
        Shild.gameObject.SetActive(!isShildCharge);
        Charging.gameObject.SetActive(!isShildCharge);
        ShildCharge.gameObject.SetActive(isShildCharge);
    }
    private void UpdateJumpAbleStatus()
    {
        grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.001f, groundCheckLayer);
        isWallRight = Physics2D.OverlapCapsule(
            wallCheckRight.position,                  // 중심
            new Vector2(0.2f, 0.6f),                  // 캡슐 크기 (너비, 높이)
            CapsuleDirection2D.Vertical,             // 방향: 세로로 길게
            0f,                                       // 회전 없음
            groundCheckLayer);                          // 감지 레이어

        isWallLeft = Physics2D.OverlapCapsule(
            wallCheckLeft.position,
            new Vector2(0.2f, 0.6f),
            CapsuleDirection2D.Vertical,
            0f,
            groundCheckLayer);
        if (isWallRight && Input.GetKey(KeyCode.D) || isWallLeft && Input.GetKey(KeyCode.A))
        {
            float slideSpeed = -2f;
            if (Input.GetKey(KeyCode.W))
                slideSpeed = -0.5f;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, slideSpeed, float.MaxValue)); //벽 슬라이드
            moveSpeed = 0f;
        }
        else if(isWallRight || isWallLeft)
            moveSpeed = 1.5f;
        else
            moveSpeed = speed;
        

        jumpAble = grounded || isWallLeft || isWallRight; //현재 문제점, 꼭지점에 닿으면 grounded, isWall 전부 false상태
        
    }

    //private void SlideWall()
    //{
    //    if (!grounded && (isWallLeft || isWallRight))
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -2f, float.MaxValue)); // 느리게 떨어지기
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MapEnd"))
        {
            isWallJumping = true;
            wallJumpTimer = wallJumpDuration;
        }
    }
}
