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
            php.PlayerHpValue(dmg);
            if (php.isShild)
                return;
        }

        gameObject.SetActive(false);
    }
}
