using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArmAndGunController : MonoBehaviour
{
    private bool isRight = true;
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

    public GameObject bulletCheckObj;
    private GameObject[] bulletCheck;
    public float[] bulletCheckX;
    public float bulletCheckY;
    //private Transform[] bulletCheckPos;
    void Start()
    {
        int col = gameObject.GetComponent<PlayerColor>().Col;
        bulletCount = PlayerPrefs.GetInt($"BulletCount{col}");
        if (bulletCount <= 0)
            bulletCount = 1;
        ReloadTime = PlayerPrefs.GetFloat($"BulletReloadTime{col}");
        Reload = ReloadTime;
        bulletSpeed = PlayerPrefs.GetFloat($"BulletSpeed{col}");
        cam = Camera.main;
        bulletOver = new List<GameObject>();
        bulletOut = new List<GameObject>();
        bulletCheck = new GameObject[bulletCount];

        for(int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = BulletCreate(BulletFactory, gameObject);

            bulletOver.Add(bullet);
            GameObject bulletCnt = Instantiate(bulletCheckObj);
            bulletCnt.transform.parent = armTransform;//armAndGun Transform에 자식 오브젝트로 추가
            bulletCnt.transform.localPosition = new Vector3(bulletCheckX[i % 3], bulletCheckY);
            bulletCheck[i] = bulletCnt;
            if (i % 3 == 2)
                bulletCheckY += 0.2f;
        }

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
        if (mouseWorldPos.x < gameObject.transform.position.x && isRight)
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
            BulletCheckPos();
            isRight = false;
        }
        else if(mouseWorldPos.x >= gameObject.transform.position.x && !isRight)
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
            BulletCheckPos();
            isRight = true;
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
            bulletCheck[bulletOver.Count].SetActive(false);//이미 bulletOver의 개수가 -1상태이기 때문에 굳이 -1할 필요 없음
            bulletOut.Add(bullet);

            Reload = ReloadTime;
        }


        Reload -= Time.deltaTime;
        if( Reload <= 0 && bulletOver.Count < bulletCount) //코루틴을 쓰지 않는 이유는 총을 쏠 때마다 리로드 시간을 초기화 시켜야하기 때문
        {
            BulletReload();
        }
    }

    static GameObject BulletCreate(GameObject BulletFactory, GameObject gameObject)
    {
        GameObject bullet = Instantiate(BulletFactory);
        Bullet bulletAct = bullet.GetComponent<Bullet>();
        bulletAct.player = gameObject;
        bullet.SetActive(false);
        return bullet;
    }
    void BulletReload()
    {
        int i = 0;
        while (bulletOver.Count < bulletCount)
        {
            GameObject bullet = bulletOut[i];
            //bullet.SetActive(false);
            if (bullet.activeSelf == true) //만약 아직 활동중인 총알일 경우 탄알집에 넣기 위한 새로운 총알 생성
            {
                bullet = BulletCreate(BulletFactory, gameObject);
                i++;
            }
            else
                bulletOut.Remove(bulletOut[i]);
            bulletCheck[bulletOver.Count].SetActive(true);//bulletOver을 Add하기 전에 SetActive를 true해놓으면 굳이 -1할 필요 없음.
                                                          //만약 bulletOver를 Add한 후에 bulletCheck를 true로 하고 싶으면 반드시 -1할 것
            bulletOver.Add(bullet);

        }
    }

    void BulletCheckPos()
    {
        for(int i = 0; i < bulletCount; i++)
        {
            Vector3 pos = bulletCheck[i].transform.localPosition;
            pos.y = -pos.y;
            bulletCheck[i].transform.localPosition = pos;
        }
    }
}
