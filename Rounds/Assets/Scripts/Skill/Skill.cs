using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skill : MonoBehaviour
{
    public float Hp;
    public float bulletDmg;
    public float bulletSpeed;
    public float bulletReloadTime;
    public GameObject[] skillCard;
    private Vector3 baseScale;

    public string[] skillName;
    
    Dictionary<int, string> card;

    private int choice;

    public Animator CardChoice;

    public Sprite[] CardImage;

    private bool isChoice;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("HP"))
            SkillSetting();

        baseScale = new Vector3(0.8f, 0.8f, 0.8f);
        //딕셔너리 쓰자. 딕셔너리로 대충 랜덤으로 뭐가 들어갔는지 key로 저장시키는거야 그러면 아마 중복도 안될듯?
        //var card = new HashSet<string>();
        card = new Dictionary<int, string>();
        int i = 0;
        while(card.Count < 5)
        {
            int rnd = Random.Range(0, skillName.Length);

            if (!card.ContainsValue(skillName[rnd]))
            {
                card.Add(i, skillName[rnd]);
                i++;
            }

        }
        skillCard[choice].transform.localScale = Vector3.one;

        foreach (var s in card.Values)
        {
            Debug.Log(s);
        }
    }

    private void Update()
    {

        if(choice < 4 && Input.GetKeyDown(KeyCode.D))
        {
            skillCard[choice].transform.localScale = baseScale;
            choice++;
            skillCard[choice].transform.localScale = Vector3.one;
        }

        if (choice > 0 && Input.GetKeyDown(KeyCode.A))
        {
            skillCard[choice].transform.localScale = baseScale;
            choice--;
            skillCard[choice].transform.localScale = Vector3.one;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isChoice)
        {
            isChoice = true;
            SkillCard();
            CardChoice.SetBool("isChoice", true);
            Invoke("StartGame", 2f);
        }
    }
    void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    void SkillSetting()
    {
        PlayerPrefs.SetFloat("HP", Hp);
        PlayerPrefs.SetFloat("BulletSpeed", bulletSpeed);
        PlayerPrefs.SetFloat("BulletReloadTime", bulletReloadTime);
        PlayerPrefs.SetFloat("BulletDmg", bulletDmg);
        PlayerPrefs.SetInt("BulletCount", 3);
    }

    void SkillCard()
    {
        int i;
        float f;
        switch (card[choice])
        {
            case "FastBall":
                f = PlayerPrefs.GetFloat("BulletSpeed");
                f += (f * 2.5f);
                PlayerPrefs.SetFloat("BulletSpeed", f);
                f = PlayerPrefs.GetFloat("BulletReloadTime");
                f += 0.25f;
                PlayerPrefs.SetFloat("BulletReloadTime", f);
                PlayerPrefs.SetString("FastBall", "True");

                break;
            case "QuickShot":
                f = PlayerPrefs.GetFloat("BulletSpeed");
                f *= 1.5f;
                PlayerPrefs.SetFloat("BulletSpeed", f);
                f = PlayerPrefs.GetFloat("BulletReloadTime");
                f += 0.25f;
                PlayerPrefs.SetFloat("ReloadTime", f);
                break;
            case "Grow":
                i = PlayerPrefs.GetInt("Grow");
                i++;
                PlayerPrefs.SetInt("Grow", i);
                break;
            case "Riccochet":
                i = PlayerPrefs.GetInt("Bounce");
                i += 2;
                PlayerPrefs.SetInt("Bounce", i);
                break;
            case "Poison":
                i = PlayerPrefs.GetInt("BulletCount");
                if (i > 1)
                    i--;
                PlayerPrefs.SetInt("BulletCount", i);
                i = PlayerPrefs.GetInt("Poison");
                i++;
                PlayerPrefs.SetInt("Poison", i);
                break;
            case "Tank":
                f = PlayerPrefs.GetFloat("HP");
                f += Hp;
                PlayerPrefs.SetFloat("HP", f);
                f = PlayerPrefs.GetFloat("BulletReloadTime");
                f += 0.5f;
                PlayerPrefs.SetFloat("BulletReloadTime", f);
                break;
            
            case "Leech":
                f = PlayerPrefs.GetFloat("HP");
                f += (f * (30 * 0.01f));
                PlayerPrefs.SetFloat("HP", f);
                PlayerPrefs.SetString("Leech", "True");
                break;
            default:
                break;
        }
    }

}
