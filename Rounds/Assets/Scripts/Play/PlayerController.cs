using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private bool jumpAble = false; //���� ���ɿ���

    public Transform groundCheckTransform;//�� ����
    public LayerMask groundCheckLayer;
    private bool grounded;

    public Transform wallCheckRight;//������ �� ����
    public Transform wallCheckLeft; //���� �� ����
    private bool isWallRight = false;
    private bool isWallLeft = false;

    public Image HpBar;
    public int MaxHp; //���� ü��, �⺻�� 10
    private float HpValue;

    private void Start()
    {
        wallJumpSpeed = jumpPower * 0.5f;
        wallJumpPower = jumpPower - 0.5f;
        moveSpeed = speed;
        HpValue = MaxHp;
    }

    private void Update()
    {
        if (jumpAble && Input.GetKeyDown(KeyCode.Space)) //GetKeyDown�� �ݵ�� Update���� ����Ѵ�.
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
                    //�� ����: x�� �� �ݴ� ����, y�� ��
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
    }

    private void FixedUpdate() //fixedUpdate�� �ַ� ���������� ������ �� �ֵ鸸 �ִ´�.
    {
        float h = Input.GetAxis("Horizontal");

        Vector2 vector = rb.velocity;
        
        vector.x = h * moveSpeed;


        if(!isWallJumping)
            rb.velocity = vector;

        UpdateJumpAbleStatus();

        SlideWall();
    }

    private void UpdateJumpAbleStatus()
    {
        grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.001f, groundCheckLayer);
        isWallRight = Physics2D.OverlapCapsule(
            wallCheckRight.position,                  // �߽�
            new Vector2(0.2f, 0.6f),                  // ĸ�� ũ�� (�ʺ�, ����)
            CapsuleDirection2D.Vertical,             // ����: ���η� ���
            0f,                                       // ȸ�� ����
            groundCheckLayer);                          // ���� ���̾�

        isWallLeft = Physics2D.OverlapCapsule(
            wallCheckLeft.position,
            new Vector2(0.2f, 0.6f),
            CapsuleDirection2D.Vertical,
            0f,
            groundCheckLayer);
        if (grounded)
        {
            moveSpeed = speed;
        }
        else if (isWallRight || isWallLeft)
            moveSpeed = 1.5f;

        jumpAble = grounded || isWallLeft || isWallRight; //���� ������, �������� ������ grounded, isWall ���� false����
        
    }

    private void SlideWall()
    {
        if (!grounded && (isWallLeft || isWallRight))
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -2f, float.MaxValue)); // ������ ��������
        }
    }

    void PlayerHpValue(float dmg)
    {
        HpValue -= dmg;
        HpValue = Mathf.Clamp(HpValue, 0, MaxHp);
        HpBar.fillAmount = HpValue/MaxHp;
    }

}
