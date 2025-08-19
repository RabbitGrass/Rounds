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
            if (PlayerPrefs.HasKey("Leech"))//��ġ ��ų�� ������ ��� ����
            {
                leech.PlayerHpValue(-(dmg * (75 * 0.01f)));
            }

            if (php.isShild)//���� ��밡 �ǵ带 ���� ���
            {
                // �ǵ� ���¸� ƨ�� ó��
                Vector2 bounceDir = (transform.position - collision.transform.position).normalized;
                rb.velocity = bounceDir * Mathf.Max(rb.velocity.magnitude, 5f);
                // �ּ� �ӵ� 5f�� ƨ���� �ʹ� ������ �ʰ� ����
            }
            else
                gameObject.SetActive(false);
        }


        if(bounce <= 0)
            gameObject.SetActive(false);
        bounce--;
    }
}
