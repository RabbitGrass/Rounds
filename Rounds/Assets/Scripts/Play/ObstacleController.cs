using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float damage;
    public float knockbackForce; // 원하는 세기로 조절

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        HpController hp = collision.gameObject.GetComponent<HpController>();
        if (hp != null)
            hp.PlayerHpValue(damage);

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            rb.velocity = Vector2.zero;
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            //rb.velocity = direction * knockbackForce;
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    HpController hp = collision.gameObject.GetComponent<HpController>();
    //    if (hp != null)
    //        hp.PlayerHpValue(damage);
    //}
}
