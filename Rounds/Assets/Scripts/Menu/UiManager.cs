using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public void StartButton()
    {
        PlayerPrefs.DeleteAll();

        int i = Random.Range(0, 2);

        PlayerPrefs.SetInt("Player", i);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Skill");
    }
}
