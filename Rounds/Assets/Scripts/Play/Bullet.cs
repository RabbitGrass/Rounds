using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float dmg;
    public GameObject player;
    public Poison poison;
    public Parasite parasite;
    private Rigidbody2D rb;
    private HpController leech;
    private int bounce;
    private void Start()
    {
        leech = player.GetComponent<HpController>();
        //Debug.Log(PlayerPrefs.HasKey("Leech"));
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerPrefs.GetFloat("BulletDmg");
        if (PlayerPrefs.HasKey("FastBall"))
        {
            rb.gravityScale = 0;
        }

        int pos = PlayerPrefs.GetInt("Poison");
        if(pos > 0)
        {
            poison.enabled = true;
        }

        int pas = PlayerPrefs.GetInt("Parasite");
        if (pas > 0)
        {
            parasite.enabled = true;
            parasite.player = player;
        }
    }

    private void OnEnable()
    {
        bounce = PlayerPrefs.GetInt("Bounce");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HpController php = collision.gameObject.GetComponent<HpController>();

        if (php != null)
            php.PlayerHpValue(dmg);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.HasKey("Leech") && collision.gameObject != player)//��ġ ��ų�� ������ ��� ����
            {
                leech.PlayerHpValue(-(dmg * (75 * 0.01f)));
            }

            if (php.isShild)//���� ��밡 �ǵ带 ���� ���
            {
                // �ǵ� ���¸� ƨ�� ó��
                Vector2 bounceDir = (transform.position - collision.transform.position).normalized;
                rb.velocity = bounceDir * Mathf.Max(rb.velocity.magnitude, 7.5f);
                // �ּ� �ӵ� 5f�� ƨ���� �ʹ� ������ �ʰ� ����
                return;
            }
            else
                gameObject.SetActive(false);
        }


        if(bounce <= 0)
            gameObject.SetActive(false);
        bounce--;
    }
}
