using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public Toggle SoundMute;
    public Slider SoundValue;
    public AudioSource Sound;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicValue"))
        {
            SoundValue.value = PlayerPrefs.GetFloat("MusicValue");
            Sound.volume = SoundValue.value;
            int i = PlayerPrefs.GetInt("MusicMute");
            if (i > 0)
            {
                Sound.mute = true;
                SoundMute.isOn = true;
            }
            else
            {
                Sound.mute = false;
                SoundMute.isOn = false;
            }
        }
    }

    public Animator SettingAni;
    //private void Update()
    //{
    //    if (SettingAni.GetBool("isSetting") && Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        SettingAni.SetBool("isSetting", false);
    //    }
    //}
    public void StartButton()
    {
        PlayerPrefs.DeleteAll();

        if (Sound.mute)
            PlayerPrefs.SetInt("MusicMute", 1);
        else
            PlayerPrefs.SetInt("MusicMute", 0);

        PlayerPrefs.SetFloat("MusicValue", Sound.volume);
        int i = Random.Range(0, 2);

        PlayerPrefs.SetInt("Player", i);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Skill");
    }

    public void SettingButton()
    {
        bool isSetting = SettingAni.GetBool("isSetting");
        SettingAni.SetBool("isSetting", !isSetting);

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
