using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickShotCard : MonoBehaviour, IsSkills
{
    public void DoSkill()
    {
        float Speed;
        Speed = PlayerPrefs.GetFloat("BulletSpeed");
        Speed *= 1.5f;
        PlayerPrefs.SetFloat("BulletSpeed", Speed);
        float Reload;
        Reload = PlayerPrefs.GetFloat("BulletReloadTime");
        Reload += 0.25f;
        PlayerPrefs.SetFloat("BulletReloadTime", Reload);
    }
}
