using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    public Image HpBar;
    public float MaxHp; //���� ü��, �⺻�� 10
    private float HpValue;
    public bool isShild = false;
    private void Start()
    {
        HpValue = MaxHp;
    }

    public void PlayerHpValue(float dmg)
    {
        if (isShild)
            return;
        HpValue -= dmg;
        HpValue = Mathf.Clamp(HpValue, 0, MaxHp);
        HpBar.fillAmount = HpValue / MaxHp;
        if (HpBar.fillAmount <= 0)
            gameObject.SetActive(false);
    }
}
