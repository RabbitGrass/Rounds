using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArmAndGunController : MonoBehaviour
{
    private bool isRight = true;
    public Transform armTransform;      // �� ������Ʈ
    //public Transform bodyTransform;     // ���� �߽� (��)
    public float armLength;//0.5      // ���� ����
    public SpriteRenderer armRenderer; //���� ��������Ʈ ������
    public SpriteRenderer playerRenderer; //�÷��̾� ���� ��������Ʈ ������
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
            bulletCnt.transform.parent = armTransform;//armAndGun Transform�� �ڽ� ������Ʈ�� �߰�
            bulletCnt.transform.localPosition = new Vector3(bulletCheckX[i % 3], bulletCheckY);
            bulletCheck[i] = bulletCnt;
            if (i % 3 == 2)
                bulletCheckY += 0.2f;
        }

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

        Vector3 gun = GunTransform.localPosition;

        //Vector3 Rotate = Bounded.transform.localEulerAngles;
        localShild = ShildCharge.localPosition;
        // �¿� ���� ó��
        if (mouseWorldPos.x < gameObject.transform.position.x && isRight)
        {
            // ���콺�� ���� �� ����
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
            // ���콺�� ������ �� ������
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
            bulletCheck[bulletOver.Count].SetActive(false);//�̹� bulletOver�� ������ -1�����̱� ������ ���� -1�� �ʿ� ����
            bulletOut.Add(bullet);

            Reload = ReloadTime;
        }


        Reload -= Time.deltaTime;
        if( Reload <= 0 && bulletOver.Count < bulletCount) //�ڷ�ƾ�� ���� �ʴ� ������ ���� �� ������ ���ε� �ð��� �ʱ�ȭ ���Ѿ��ϱ� ����
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
            if (bullet.activeSelf == true) //���� ���� Ȱ������ �Ѿ��� ��� ź������ �ֱ� ���� ���ο� �Ѿ� ����
            {
                bullet = BulletCreate(BulletFactory, gameObject);
                i++;
            }
            else
                bulletOut.Remove(bulletOut[i]);
            bulletCheck[bulletOver.Count].SetActive(true);//bulletOver�� Add�ϱ� ���� SetActive�� true�س����� ���� -1�� �ʿ� ����.
                                                          //���� bulletOver�� Add�� �Ŀ� bulletCheck�� true�� �ϰ� ������ �ݵ�� -1�� ��
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
