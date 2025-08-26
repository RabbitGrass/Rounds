using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float bounceForce;
    private float bounce;
    public Vector2 bounceDirection; // ������ Inspector���� ���� (ex: ���� ���� (1,0))
    Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController shild = other.GetComponent<PlayerController>();
        if (shild.isShild)
            bounce = bounceForce * 1.5f;
        else
        {
            bounce = bounceForce;
        }

        HpController Hp = other.GetComponent<HpController>();

        Hp.PlayerHpValue(4f);

        rb = other.attachedRigidbody;

        // ���� �ӵ� ���� (Ư�� ƨ��� ���� ���и� �����ص� ��)
        rb.velocity = Vector2.zero;

        // ƨ��� �� �ֱ�
        //rb.AddForce(bounceDirection.normalized * bounceForce, ForceMode2D.Impulse);
        rb.velocity = bounceDirection.normalized * bounce;

        // ��ġ ���� (��¦ �� �������� �о�ֱ�)
        other.transform.position = (Vector2)other.transform.position + bounceDirection.normalized * 0.1f;

        Invoke("BounceStop", 0.1f);
    }

    void BounceStop()
    {
        rb.velocity = Vector2.zero;
    }
}
