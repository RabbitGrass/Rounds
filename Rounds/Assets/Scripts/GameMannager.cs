using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMannager : MonoBehaviour
{
    public Texture2D cursorIcon;

    public Image[] OrangeWinCount;
    public Image[] BlueWinCount;
    int OWin;
    int BWin;

    public GameObject[] Player;

    private bool RoundEnd;
    void Start()
    {
        int Col = PlayerPrefs.GetInt("Player");

        PlayerController playerCnt = Player[Col].GetComponent<PlayerController>();
        ArmAndGunController arm = Player[Col].GetComponent<ArmAndGunController>();

        playerCnt.enabled = true;
        arm.enabled = true;
        
        Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto);
        OWin = PlayerPrefs.GetInt("OrangeWinCount");
        BWin = PlayerPrefs.GetInt("BlueWinCount");

        for(int i = 0; i < OWin; i++)
        {
            OrangeWinCount[i].gameObject.SetActive(true);
        }
        for(int i = 0; i < BWin; i++)
        {
            BlueWinCount[i].gameObject.SetActive(true);
        }
    }

    public void WhoAreWin(GameObject player)
    {
        if (RoundEnd)
            return;
        RoundEnd = true;
        PlayerColor playerColor = player.GetComponent<PlayerColor>();
        int col = playerColor.Col;

        if(col == 1)//�й����� ��
        {
            OrangeWinCount[OWin++].gameObject.SetActive(true);
            PlayerPrefs.SetInt("OrangeWinCount", OWin);
            PlayerPrefs.SetInt("ChoicePlayer", col);
        }
        else
        {
            BlueWinCount[BWin++].gameObject.SetActive(true);
            PlayerPrefs.SetInt("BlueWinCount", BWin);
            PlayerPrefs.SetInt("ChoicePlayer", col);
        }

        if(OWin >= 5 || BWin >= 5)
        {
            Time.timeScale = 0.5f;
        }
        else
            Invoke("NextRound", 3f);
    }

    void NextRound() => SceneManager.LoadScene("Skill");

}
