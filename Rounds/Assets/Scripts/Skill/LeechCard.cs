using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeechCard : MonoBehaviour, IsSkills
{
   public void DoSkill()
    {
        float hp = 0;
        hp = PlayerPrefs.GetFloat("HP");
        hp += (hp * (30 * 0.01f));
        PlayerPrefs.SetFloat("HP", hp);
        PlayerPrefs.SetString("Leech", "True"); //HasKey로 존재 여부만 볼 예정
    }
}
