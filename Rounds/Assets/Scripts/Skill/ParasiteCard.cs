using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteCard : MonoBehaviour, IsSkills
{
    public void DoSkill(int player)
    {
        float BulletDmg = 0;
        BulletDmg = PlayerPrefs.GetFloat($"BulletDmg{player}");
        BulletDmg -= 0.5f;
        PlayerPrefs.SetFloat($"BulletDmg{player}", BulletDmg);
        int i = 0;
        i = PlayerPrefs.GetInt($"Parasite{player}");
        i++;
        PlayerPrefs.SetInt($"Parasite{player}", i);
    }
}
