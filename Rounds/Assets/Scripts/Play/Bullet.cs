using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;
    public GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HpController php = collision.gameObject.GetComponent<HpController>();
            Rigidbody2D prb = collision.gameObject.GetComponent<Rigidbody2D>();
            php.PlayerHpValue(dmg);
            if (php.isShild)
            {
                if (prb != null)
                {
                    // 충돌 법선 방향의 속도만 제거
                    Vector2 normal = collision.GetContact(0).normal;
                    Vector2 vel = prb.velocity;
                    Vector2 rebound = Vector2.Dot(vel, normal) * normal;
                    prb.velocity = vel - rebound;
                }
                return;
            }
        }

        gameObject.SetActive(false);
    }
}
