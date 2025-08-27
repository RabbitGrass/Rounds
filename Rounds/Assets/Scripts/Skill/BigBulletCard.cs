using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBulletCard : MonoBehaviour, IsSkills
{
    public void DoSkill(int player)
    {
        int i = PlayerPrefs.GetInt($"BigBullet{player}");
        i++;
        PlayerPrefs.SetInt($"BigBullet{player}", i);
    }
}
