using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCard : MonoBehaviour, IsSkills
{
    public void DoSkill()
    {
        int bulletCount = 0;
        bulletCount = PlayerPrefs.GetInt("BulletCount");
        if (bulletCount > 1)
            bulletCount--;
        PlayerPrefs.SetInt("BulletCount", bulletCount);
        bulletCount = PlayerPrefs.GetInt("Poison");
        bulletCount++;
        PlayerPrefs.SetInt("Poison", bulletCount);
    }
}
