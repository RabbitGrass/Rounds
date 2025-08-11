using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float bounceForce = 12f;
    public Vector2 bounceDirection = Vector2.zero; // 벽마다 Inspector에서 설정 (ex: 왼쪽 벽은 (1,0))
    Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        HpController Hp = other.GetComponent<HpController>();

        Hp.PlayerHpValue(4f);

        rb = other.attachedRigidbody;

        // 기존 속도 제거 (특히 튕기는 방향 성분만 제거해도 됨)
        rb.velocity = Vector2.zero;

        // 튕기는 힘 주기
        //rb.AddForce(bounceDirection.normalized * bounceForce, ForceMode2D.Impulse);
        rb.velocity = bounceDirection.normalized * bounceForce;

        // 위치 보정 (살짝 벽 안쪽으로 밀어넣기)
        other.transform.position = (Vector2)other.transform.position + bounceDirection.normalized * 0.1f;

        Invoke("BounceStop", 0.1f);
    }

    void BounceStop()
    {
        rb.velocity = Vector2.zero;
    }
}
