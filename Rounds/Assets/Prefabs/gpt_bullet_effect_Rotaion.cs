using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EffectFollowRotation : MonoBehaviour
{
    public Transform bulletTransform;
    public float rotationSpeed = 10f; // �ʿ� �� �ε巴�� ȸ���Ϸ��� ���

    public void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * 100);
    }
    void Update()
    {
        // �Ѿ��� �ӵ� ����(������ ���ϴ� ����)�� ��������
        Vector2 moveDirection = bulletTransform.GetComponent<Rigidbody2D>().velocity;

        // �̵� ���� ���� ȸ��
        if (moveDirection != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = targetRotation;
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}