using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float bounceForce;

    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 dir = Vector2.zero;

                // 위치에 따라 튕기는 방향 결정
                if (rb.position.x < minX) dir = Vector2.right;  // 왼쪽 경계
                else if (rb.position.x > maxX) dir = Vector2.left;   // 오른쪽 경계
                else if (rb.position.y < minY) dir = Vector2.up;     // 아래 경계
                else if (rb.position.y > maxY) dir = Vector2.down;   // 위 경계

                rb.velocity = Vector2.zero;
                rb.AddForce(dir * bounceForce, ForceMode2D.Impulse);

                // 위치 보정
                other.transform.position = rb.position + dir * 0.5f;
            }
        }
    }
}
