using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteCard : MonoBehaviour, IsSkills
{
    public void DoSkill()
    {
        float BulletDmg = 0;
        BulletDmg = PlayerPrefs.GetFloat("BulletDmg");
        BulletDmg -= 0.5f;
        PlayerPrefs.SetFloat("BulletDmg", BulletDmg);
        int i = 0;
        i = PlayerPrefs.GetInt("Parasite");
        i++;
        PlayerPrefs.SetInt("Parasite", i);
    }
}
