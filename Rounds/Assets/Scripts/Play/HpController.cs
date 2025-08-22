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
}
