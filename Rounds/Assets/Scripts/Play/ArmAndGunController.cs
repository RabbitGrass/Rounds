using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject[] bulletCheck;
    //private Transform[] bulletCheckPos;
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
        //Debug.Log(Reload);
        if( Reload <= 0)
        {
            while(bulletOut.Count > 0)
            {
                GameObject bullet = bulletOut[0];
                bullet.SetActive(false);
                bulletCheck[bulletOver.Count].SetActive(true);//bulletOver�� Add�ϱ� ���� SetActive�� true�س����� ���� -1�� �ʿ� ����.
                                                              //���� bulletOver�� Add�� �Ŀ� bulletCheck�� true�� �ϰ� ������ �ݵ�� -1�� ��
                bulletOver.Add(bullet);
                bulletOut.Remove(bullet);
                
            }
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
