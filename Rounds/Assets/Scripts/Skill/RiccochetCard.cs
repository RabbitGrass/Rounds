using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiccochetCard : MonoBehaviour, IsSkills
{
   public void DoSkill(int player)
    {
        int i = 0;
        i = PlayerPrefs.GetInt($"Bounce{player}");
        i += 2;
        PlayerPrefs.SetInt($"Bounce{player}", i);
    }
}
