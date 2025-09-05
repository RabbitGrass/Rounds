using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMannager : MonoBehaviour
{
    public static GameMannager gm;

    private void Awake() //���� gm�� �̱������� ����� ���� ��
    {
        if (gm == null)
            gm = this;
    }
    public enum GameState
    {
        RoundIdle,
        RoundStart,
        RoundOver,
        GameOver
    }
    public GameState gState;

    public Texture2D cursorIcon;

    public Image[] OrangeWinCount;
    public Image[] BlueWinCount;

    public GameObject Winner;
    int OWin;
    int BWin;

    private int playerCol;
    public GameObject[] Player;

    public GameObject RestartChoice;
    public Text[] RestartText;
    public int textAlpha = 135;
    private bool ChoiceRestart;

    public GameObject Setting;
    void Start()
    {
        playerCol = PlayerPrefs.GetInt("Player");
        gState = GameState.RoundIdle; //�ִϸ��̼��� ���۵� ���� ���
        Invoke("RoundStart", 2f);

        PlayerController playerCnt = Player[playerCol].GetComponent<PlayerController>();
        ArmAndGunController arm = Player[playerCol].GetComponent<ArmAndGunController>();

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
        //�÷��̾ �� ������ �����ߴ��� ����
        ChoiceRestart = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Setting.activeSelf)
                Setting.SetActive(true);
            else
                Setting.SetActive(false);
        }

        if (gState != GameState.GameOver)
            return; //���ӿ��� �Ǿ��� ��쿡�� ����

        if (Input.GetKeyDown(KeyCode.D)) //No
        {
            RestartText[1].fontSize = 100;
            RestartText[1].color = new Color(255, 255, 255, 255);
            RestartText[0].fontSize = 50;
            RestartText[0].color = new Color(255, 255, 255, textAlpha);
            ChoiceRestart = false;
        }
        else if (Input.GetKeyDown(KeyCode.A)) //Yes
        {
            RestartText[0].fontSize = 100;
            RestartText[0].color = new Color(255, 255, 255, 255);
            RestartText[1].fontSize = 50;
            RestartText[1].color = new Color(255, 255, 255, textAlpha);
            ChoiceRestart = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            IsRestart(ChoiceRestart);
        }

    }

    public void WhoAreWin(GameObject player)
    {
        if (gState == GameState.RoundOver || gState == GameState.GameOver)
            return;
        gState = GameState.RoundOver; //���� ������ �ٲ� ���̻� WoeAreWin�Լ��� ������� �ʰ� �Ѵ�.
        PlayerColor playerColor = player.GetComponent<PlayerColor>();
        int col = playerColor.Col;

        if(col == 1)//�й����� ��
        {
            OrangeWinCount[OWin++].gameObject.SetActive(true);
            PlayerPrefs.SetInt("OrangeWinCount", OWin);
            PlayerPrefs.SetInt("ChoicePlayer", col);
            Image WinnerCol = Winner.GetComponentInChildren<Image>();
            WinnerCol.color = new Color32(255, 144, 0, 255);
            Text WinnerText = Winner.GetComponentInChildren<Text>();
            WinnerText.color = new Color32(255, 144, 0, 255);
            Winner.SetActive(true);
        }
        else
        {
            BlueWinCount[BWin++].gameObject.SetActive(true);
            PlayerPrefs.SetInt("BlueWinCount", BWin);
            PlayerPrefs.SetInt("ChoicePlayer", col);
            Image WinnerCol = Winner.GetComponentInChildren<Image>();
            WinnerCol.color = new Color32(0, 107, 255, 255);
            Text WinnerText = Winner.GetComponentInChildren<Text>();
            WinnerText.color = new Color32(0, 107, 255, 255);
            Winner.SetActive(true);
        }

        if(OWin >= 5 || BWin >= 5)
        {
            Invoke("GameOver", 0.5f);
            PlayerPrefs.DeleteAll(); //��� ������ �������Ƿ� ��� ��ų �� ������ �ʱ�ȭ
            Time.timeScale = 0.3f;
        }
        else
            Invoke("NextRound", 3f);
    }

    void IsRestart(bool Restart)
    {
        Time.timeScale = 1f;
        if (Restart)
        {
            PlayerPrefs.SetInt("Player", playerCol);
            PlayerPrefs.Save();
            NextRound(); //���� ���� ����
        }
        else
        {
            MainMenu();
        }
    }
    void GameOver()
    {
        Winner.SetActive(false);
        RestartChoice.SetActive(true);
        gState = GameState.GameOver;
    }

    void RoundStart() => gState = GameState.RoundStart;
    void NextRound() => SceneManager.LoadScene("Skill");

    public void MainMenu() => SceneManager.LoadScene("Menu");

    //���� ���� �ɼ�
    public void QuitGame()
    {
#if UNITY_EDITOR //��ó����, ����Ƽ �󿡼� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //���ø����̼��� ����
        Application.Quit();
#endif
    }

}
