using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float dmg;
    public GameObject player;
    private Rigidbody2D rb;
    private HpController leech;
    private int bounce;
    private void Start()
    {
        leech = player.GetComponent<HpController>();
        Debug.Log(PlayerPrefs.HasKey("Leech"));
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerPrefs.GetFloat("BulletDmg");
        if (PlayerPrefs.HasKey("FastBall"))
        {
            rb.gravityScale = 0;
        }
    }

    private void OnEnable()
    {
        bounce = PlayerPrefs.GetInt("Bounce");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HpController php = collision.gameObject.GetComponent<HpController>();
            Rigidbody2D prb = collision.gameObject.GetComponent<Rigidbody2D>();
            php.PlayerHpValue(dmg);
            if (php.isShild)//���� ��밡 �ǵ带 ���� ���
            {
                if (prb != null) //�� �κ��� �÷��̾ �ǵ� ����� �� �ѿ� ���� ��� �ڷ� �з����� �ݵ��� ���ַ��� �� �ǵ�, ��ȭ�� �Ǿ����� ���������� �ʾ���, �ٸ���� ���� �ʿ�
                {
                    // �浹 ���� ������ �ӵ��� ����
                    Vector2 normal = collision.GetContact(0).normal;
                    Vector2 vel = prb.velocity;
                    Vector2 rebound = Vector2.Dot(vel, normal) * normal;
                    prb.velocity = vel - rebound;
                }
                return;
            }
            if (PlayerPrefs.HasKey("Leech"))//��ġ ��ų�� ������ ��� ����
            {
                leech.PlayerHpValue(-(dmg * (75 * 0.01f)));
            }

            gameObject.SetActive(false);
        }


        if(bounce <= 0)
            gameObject.SetActive(false);
        bounce--;
    }
}
