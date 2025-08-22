using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float damage;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        HpController hp = collision.gameObject.GetComponent<HpController>();

        hp.PlayerHpValue(damage);

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>(); 
        Vector2 direction = collision.gameObject.transform.position - transform.position;

        Debug.Log(direction.normalized);

        direction = direction.normalized * 500;
        rb.AddForce(direction);
    }
}
