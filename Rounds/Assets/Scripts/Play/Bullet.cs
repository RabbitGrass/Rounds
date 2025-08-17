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
            if (php.isShild)//만약 상대가 실드를 썼을 경우
            {
                if (prb != null) //이 부분은 플레이어가 실드 사용한 후 총에 맞을 경우 뒤로 밀려나는 반동을 없애려고 한 건데, 완화는 되었지만 없어지지는 않았음, 다른방법 강구 필요
                {
                    // 충돌 법선 방향의 속도만 제거
                    Vector2 normal = collision.GetContact(0).normal;
                    Vector2 vel = prb.velocity;
                    Vector2 rebound = Vector2.Dot(vel, normal) * normal;
                    prb.velocity = vel - rebound;
                }
                return;
            }
            if (PlayerPrefs.HasKey("Leech"))//리치 스킬을 가졌을 경우 실행
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
