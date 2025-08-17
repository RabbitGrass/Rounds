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
        SceneManager.LoadScene("Skill");
    }
}
