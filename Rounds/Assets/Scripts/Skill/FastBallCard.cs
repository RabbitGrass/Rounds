using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBallCard : MonoBehaviour, IsSkills
{
    public void DoSkill(int player)
    {
        float speed;
        speed = PlayerPrefs.GetFloat($"BulletSpeed{player}");
        speed += (speed * 2.5f);
        PlayerPrefs.SetFloat($"BulletSpeed{player}", speed);
        float reload;
        reload = PlayerPrefs.GetFloat($"BulletReloadTime{player}");
        reload += 0.25f;
        PlayerPrefs.SetFloat($"BulletReloadTime{player}", reload);
        PlayerPrefs.SetString($"FastBall{player}", "True");
    }
}
