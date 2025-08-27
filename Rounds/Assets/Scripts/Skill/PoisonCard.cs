using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCard : MonoBehaviour, IsSkills
{
    public void DoSkill(int player)
    {
        int bulletCount = 0;
        bulletCount = PlayerPrefs.GetInt($"BulletCount{player}");
        if (bulletCount > 1)
            bulletCount--;
        PlayerPrefs.SetInt($"BulletCount{player}", bulletCount);
        bulletCount = PlayerPrefs.GetInt($"Poison{player}");
        bulletCount++;
        PlayerPrefs.SetInt($"Poison{player}", bulletCount);
    }
}
