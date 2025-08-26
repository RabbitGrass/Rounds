using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCard : MonoBehaviour, IsSkills
{
    public void DoSkill()
    {
        int i = 0;
        i = PlayerPrefs.GetInt("Grow");
        i++;
        PlayerPrefs.SetInt("Grow", i);
    }
}
