using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    public Image HpBar;
    public float MaxHp; //유저 체력, 기본값 10
    private float HpValue;

    private void Start()
    {
        HpValue = MaxHp;
    }

    public void PlayerHpValue(float dmg)
    {
        HpValue -= dmg;
        HpValue = Mathf.Clamp(HpValue, 0, MaxHp);
        HpBar.fillAmount = HpValue / MaxHp;
        if (HpBar.fillAmount <= 0)
            gameObject.SetActive(false);
    }
}
