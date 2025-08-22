using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    public Image HpBar;
    public float MaxHp; //유저 체력, 기본값 10
    private float HpValue;
    public bool isShild = false;
    public GameMannager GameMannager;

    private void Start()
    {
        if(gameObject.layer == LayerMask.NameToLayer("Player"))
            MaxHp = PlayerPrefs.GetFloat("HP");
        HpValue = MaxHp;
        Debug.Log("MaxHp : " + MaxHp);
    }

    public void PlayerHpValue(float dmg)
    {
        if (isShild)
            return;
        HpValue -= dmg;
        HpValue = Mathf.Clamp(HpValue, 0, MaxHp);

        if (!gameObject.CompareTag("Player"))
        {
            Debug.Log(HpValue);
            if(HpValue <= 0)
                Destroy(gameObject);
            Debug.Log(HpValue);
            return;
        }
        HpBar.fillAmount = HpValue / MaxHp;
        if (HpValue <= 0)
        {
            gameObject.SetActive(false);
            GameMannager.WhoAreWin(gameObject);
        }
    }


    public IEnumerator Poison(float poison)
    {
        int i = 8;
        while(i > 0)
        {
            yield return new WaitForSeconds(0.8f);
            PlayerHpValue(poison);
            i--;
        }
    }

    public IEnumerator Parasite(float parasite, GameObject BulletPlayer)
    {
        int i = 8;
        HpController hp;
        while (i > 0)
        {
    
            yield return new WaitForSeconds(0.8f);
            PlayerHpValue(parasite);

            if (BulletPlayer != gameObject)
            {
                hp = BulletPlayer.GetComponent<HpController>();
                hp.PlayerHpValue(-parasite);
            }            
            i--;
        }
    }
}
