using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMannager : MonoBehaviour
{
    public static GameMannager gm;

    private void Awake() //변수 gm을 싱글톤으로 만들기 위한 것
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
        gState = GameState.RoundIdle; //애니메이션이 시작될 동안 대기
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
        //플레이어가 재 시작을 선택했는지 여부
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
            return; //게임오버 되었을 경우에만 실행

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
        gState = GameState.RoundOver; //라운드 오버로 바꿔 더이상 WoeAreWin함수가 실행되지 않게 한다.
        PlayerColor playerColor = player.GetComponent<PlayerColor>();
        int col = playerColor.Col;

        if(col == 1)//패배자의 색
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
            PlayerPrefs.DeleteAll(); //모든 게임이 끝났으므로 모든 스킬 및 데이터 초기화
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
            NextRound(); //다음 게임 시작
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

    //게임 종료 옵션
    public void QuitGame()
    {
#if UNITY_EDITOR //전처리기, 유니티 상에서 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //애플리케이션을 종료
        Application.Quit();
#endif
    }

}
