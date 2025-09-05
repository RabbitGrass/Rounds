using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillGameManager : MonoBehaviour
{
    public GameObject Setting;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!Setting.activeSelf)
                Setting.SetActive(true);
            else
                Setting.SetActive(false);
        }
    }
    public void MainMenu() => SceneManager.LoadScene("Menu");

    //���� ���� �ɼ�
    public void QuitGame()
    {
        Application.Quit();
    }
}
