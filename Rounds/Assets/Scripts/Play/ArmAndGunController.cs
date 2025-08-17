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
    public Transform Charging;

    Vector3 localShild;
    private Camera cam;
    public GameObject BulletFactory;
    public List<GameObject> bulletOver;
    private List<GameObject> bulletOut;
    public Transform GunTransform;
    private int bulletCount;
    private float bulletSpeed;
    private float ReloadTime;
    private float Reload;
    void Start()
    {
        bulletCount = PlayerPrefs.GetInt("BulletCount");
        ReloadTime = PlayerPrefs.GetFloat("BulletReloadTime");
        Reload = ReloadTime;
        bulletSpeed = PlayerPrefs.GetFloat("BulletSpeed");
        cam = Camera.main;

        bulletOver = new List<GameObject>();
        bulletOut = new List<GameObject>();
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(BulletFactory);
            Bullet bulletAct = bullet.GetComponent<Bullet>();
            bulletAct.player = gameObject;

            bulletOver.Add(bullet);
            bullet.SetActive(false);
        }

        Debug.Log("count : " + bulletCount);
        Debug.Log("bulletSpeed : " + bulletSpeed);
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

        Vector3 gun = GunTransform.localPosition;

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
            gun.y = -0.25f;
            GunTransform.localPosition = gun;
            
            localShild.x = Mathf.Abs(localShild.x);
            ShildCharge.localPosition = localShild;
            Charging.localPosition = localShild;
        }
        else
        {
            // 마우스가 오른쪽 → 정방향
            armRenderer.flipY = false;
            playerRenderer.flipX = false;
            //Rotate.y = 180;
            //Bounded.transform.localEulerAngles = Rotate;
            gun.y = 0.25f;
            GunTransform.localPosition = gun;
            localShild.x = Mathf.Abs(localShild.x) * -1;
            ShildCharge.localPosition = localShild;
            Charging.localPosition = localShild;
        }


        if(bulletOver.Count > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = bulletOver[0];
            bulletOver.Remove(bullet);
            bullet.gameObject.transform.position = GunTransform.position;
            bullet.gameObject.transform.rotation = GunTransform.rotation;
            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            rb.AddForce(direction * bulletSpeed);

            bulletOut.Add(bullet);

            Reload = ReloadTime;
        }


        Reload -= Time.deltaTime;
        //Debug.Log(Reload);
        if( Reload <= 0)
        {
            while(bulletOut.Count > 0)
            {
                GameObject bullet = bulletOut[0];
                bullet.SetActive(false);
                bulletOver.Add(bullet);
                bulletOut.Remove(bullet);
            }
        }
    }
}
