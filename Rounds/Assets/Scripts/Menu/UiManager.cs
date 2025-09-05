using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{


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
