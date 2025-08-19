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
        HpBar.fillAmount = HpValue / MaxHp;
        if (HpBar.fillAmount <= 0 && gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Invoke("NextRound", 3f);
        }
    }

    void NextRound() => SceneManager.LoadScene("Skill");
}
