using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float dmg;
    public GameObject player;
    public int col;
    public Poison poison;
    public int pos;
    public Parasite parasite;
    public int par;
    private Rigidbody2D rb;
    private HpController leech;
    private int bounce;
    public GameObject[] bulletEff;
    private void Start()
    {
        col = player.GetComponent<PlayerColor>().Col;
        leech = player.GetComponent<HpController>();
        //Debug.Log(PlayerPrefs.HasKey("Leech"));
        rb = GetComponent<Rigidbody2D>();
        dmg = PlayerPrefs.GetFloat($"BulletDmg{col}");
        if (PlayerPrefs.HasKey($"FastBall{col}"))
        {
            rb.gravityScale = 0;
        }

        pos = PlayerPrefs.GetInt($"Poison{col}");
        if(pos > 0)
        {
            EffectSetting(2);    
            poison.enabled = true;
        }

        par = PlayerPrefs.GetInt($"Parasite{col}");
        if (par > 0)
        {
            EffectSetting(3);
            parasite.enabled = true;
            parasite.player = player;
        }

        if(pos <= 0 && par <= 0)
        {
            EffectSetting(0);
        }

        int big = PlayerPrefs.GetInt($"BigBullet{col}");
        if(big > 0)
        {
            EffectSetting(1);
        }
    }

    private void OnEnable()
    {
        bounce = PlayerPrefs.GetInt($"Bounce{col}");
    }

    void EffectSetting(int num)
    {
        GameObject Effect = Instantiate(bulletEff[num]);
        Effect.transform.parent = gameObject.transform;
        Effect.transform.localPosition = Vector2.zero;
        Effect.transform.localRotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HpController php = collision.gameObject.GetComponent<HpController>();

        if (php != null)
            php.PlayerHpValue(dmg);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.HasKey($"Leech{col}") && collision.gameObject != player)//��ġ ��ų�� ������ ��� ����
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
