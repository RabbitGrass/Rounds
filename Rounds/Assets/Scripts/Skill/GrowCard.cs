using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCard : MonoBehaviour, IsSkills
{
    public void DoSkill(int player)
    {
        int i = 0;
        i = PlayerPrefs.GetInt($"Grow{player}");
        i++;
        PlayerPrefs.SetInt($"Grow{player}", i);
    }
}
