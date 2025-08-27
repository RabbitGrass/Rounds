using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCard : MonoBehaviour, IsSkills
{
    public void DoSkill(int player)
    {
        float hp = 0;
        hp = PlayerPrefs.GetFloat($"HP{player}");
        hp += hp;
        PlayerPrefs.SetFloat($"HP{player}", hp);

        float Reload = 0;
        Reload = PlayerPrefs.GetFloat($"BulletReloadTime{player}");
        Reload += 0.5f;
        PlayerPrefs.SetFloat($"BulletReloadTime{player}", Reload);
    }
}
