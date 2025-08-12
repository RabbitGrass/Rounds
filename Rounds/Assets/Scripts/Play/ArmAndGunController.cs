using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAndGunController : MonoBehaviour
{
    public Transform armTransform;      // �� ������Ʈ
    //public Transform bodyTransform;     // ���� �߽� (��)
    public float armLength;//0.5      // ���� ����
    public SpriteRenderer armRenderer; //���� ��������Ʈ ������
    public SpriteRenderer playerRenderer; //�÷��̾� ���� ��������Ʈ ������
    public ParticleSystem Bounded;
    public Transform ShildCharge;
    Vector3 localShild;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // ���콺 ��ġ ��������
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        // ���� �߽� -> ���콺 ���� ����
        Vector3 direction = (mouseWorldPos - gameObject.transform.position).normalized;

        // �� ��ġ = �� �߽� + ���� * �� ����
        armTransform.position = gameObject.transform.position + direction * armLength;

        // �� ȸ�� (���� ���콺 �ٶ󺸵���)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        armTransform.rotation = Quaternion.Euler(0, 0, angle);

        //Vector3 Rotate = Bounded.transform.localEulerAngles;
        localShild = ShildCharge.localPosition;
        // �¿� ���� ó��
        if (mouseWorldPos.x < gameObject.transform.position.x)
        {
            // ���콺�� ���� �� ����
            armRenderer.flipY = true;
            playerRenderer.flipX = true;
            //Rotate.y = 0;
            //Bounded.transform.localEulerAngles = Rotate;
            localShild.x = Mathf.Abs(localShild.x);
            ShildCharge.localPosition = localShild;
        }
        else
        {
            // ���콺�� ������ �� ������
            armRenderer.flipY = false;
            playerRenderer.flipX = false;
            //Rotate.y = 180;
            //Bounded.transform.localEulerAngles = Rotate;
            localShild.x = Mathf.Abs(localShild.x) * -1;
            ShildCharge.localPosition = localShild;
        }
    }
}
