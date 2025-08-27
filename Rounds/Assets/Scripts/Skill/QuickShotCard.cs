using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickShotCard : MonoBehaviour, IsSkills
{
    public void DoSkill(int player)
    {
        float Speed;
        Speed = PlayerPrefs.GetFloat($"BulletSpeed{player}");
        Speed *= 1.5f;
        PlayerPrefs.SetFloat($"BulletSpeed{player}", Speed);
        float Reload;
        Reload = PlayerPrefs.GetFloat($"BulletReloadTime{player}");
        Reload += 0.25f;
        PlayerPrefs.SetFloat($"BulletReloadTime{player}", Reload);
    }
}
