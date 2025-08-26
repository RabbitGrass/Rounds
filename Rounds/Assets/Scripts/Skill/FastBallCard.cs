using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBallCard : MonoBehaviour, IsSkills
{
    public void DoSkill()
    {
        float speed;
        speed = PlayerPrefs.GetFloat("BulletSpeed");
        speed += (speed * 2.5f);
        PlayerPrefs.SetFloat("BulletSpeed", speed);
        float reload;
        reload = PlayerPrefs.GetFloat("BulletReloadTime");
        reload += 0.25f;
        PlayerPrefs.SetFloat("BulletReloadTime", reload);
        PlayerPrefs.SetString("FastBall", "True");
    }
}
