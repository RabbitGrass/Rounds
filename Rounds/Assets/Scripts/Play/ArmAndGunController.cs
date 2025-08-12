using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAndGunController : MonoBehaviour
{
    public Transform armTransform;      // 팔 오브젝트
    //public Transform bodyTransform;     // 몸통 중심 (원)
    public float armLength;//0.5      // 팔의 길이
    public SpriteRenderer armRenderer; //팔의 스프라이트 렌더러
    public SpriteRenderer playerRenderer; //플레이어 몸통 스프라이트 렌더러
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
        // 마우스 위치 가져오기
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        // 몸통 중심 -> 마우스 방향 벡터
        Vector3 direction = (mouseWorldPos - gameObject.transform.position).normalized;

        // 팔 위치 = 몸 중심 + 방향 * 팔 길이
        armTransform.position = gameObject.transform.position + direction * armLength;

        // 팔 회전 (팔이 마우스 바라보도록)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        armTransform.rotation = Quaternion.Euler(0, 0, angle);

        //Vector3 Rotate = Bounded.transform.localEulerAngles;
        localShild = ShildCharge.localPosition;
        // 좌우 반전 처리
        if (mouseWorldPos.x < gameObject.transform.position.x)
        {
            // 마우스가 왼쪽 → 반전
            armRenderer.flipY = true;
            playerRenderer.flipX = true;
            //Rotate.y = 0;
            //Bounded.transform.localEulerAngles = Rotate;
            localShild.x = Mathf.Abs(localShild.x);
            ShildCharge.localPosition = localShild;
        }
        else
        {
            // 마우스가 오른쪽 → 정방향
            armRenderer.flipY = false;
            playerRenderer.flipX = false;
            //Rotate.y = 180;
            //Bounded.transform.localEulerAngles = Rotate;
            localShild.x = Mathf.Abs(localShild.x) * -1;
            ShildCharge.localPosition = localShild;
        }
    }
}
