using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EffectFollowRotation : MonoBehaviour
{
    public Transform bulletTransform;
    public float rotationSpeed = 10f; // 필요 시 부드럽게 회전하려면 사용

    public void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * 100);
    }
    void Update()
    {
        // 총알의 속도 방향(앞으로 향하는 방향)을 가져오기
        Vector2 moveDirection = bulletTransform.GetComponent<Rigidbody2D>().velocity;

        // 이동 중일 때만 회전
        if (moveDirection != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = targetRotation;
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}