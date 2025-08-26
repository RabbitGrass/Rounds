using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCard : MonoBehaviour, IsSkills
{
    public void DoSkill()
    {
        Debug.Log("≈ ≈© Ω««‡");
        float hp = 0;
        hp = PlayerPrefs.GetFloat("HP");
        hp += hp;
        PlayerPrefs.SetFloat("HP", hp);

        float Reload = 0;
        Reload = PlayerPrefs.GetFloat("BulletReloadTime");
        Reload += 0.5f;
        PlayerPrefs.SetFloat("BulletReloadTime", Reload);
    }
}
