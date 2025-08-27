using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeechCard : MonoBehaviour, IsSkills
{
   public void DoSkill(int player)
    {
        float hp = 0;
        hp = PlayerPrefs.GetFloat($"HP{player}");
        hp += (hp * (30 * 0.01f));
        PlayerPrefs.SetFloat($"HP{player}", hp);
        PlayerPrefs.SetString($"Leech{player}", "True"); //HasKey로 존재 여부만 볼 예정
    }
}
