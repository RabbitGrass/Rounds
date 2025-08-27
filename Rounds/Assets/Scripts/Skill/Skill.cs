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
    public Transform[] skillCard;
    public GameObject[] skills;
    private Vector3 baseScale;

    private int choice;

    public Animator CardChoice;

    private bool isChoice;
   
    private bool ChoiceStart; //시작시 바로 움직이는 것을 방지하기 위함
    private bool isStart;

    public int ChoicePlayer;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("ChoicePlayer"))
        {
            ChoicePlayer = 0;
            isStart = true;
            SkillSetting();
        }
        else
        {
            ChoicePlayer = PlayerPrefs.GetInt("ChoicePlayer");
        }

            baseScale = new Vector3(0.8f, 0.8f, 0.8f);
        CardSetting();
    }

    private void Update()
    {
        if (!ChoiceStart)
            return;

        if (choice < 4 && Input.GetKeyDown(KeyCode.D))
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
            IsSkills skills = skillCard[choice].GetComponentInChildren<IsSkills>();
            if (skills != null)
            {
                skills.DoSkill(ChoicePlayer);
            }
            //SkillCard();
            CardChoice.SetBool("isChoice", isChoice);

            if (isStart)
            {
                PlayerPrefs.SetInt("ChoicePlayer", 1);
                Invoke("SkillRestart", 1f);
            }
            else
                Invoke("StartGame", 2f);
        }
    }

    void CardPlace()
    {
        skillCard[choice].transform.localScale = Vector3.one;
        ChoiceStart = true;
    }

    void SkillRestart()
    {
        SceneManager.LoadScene("Skill");
    }

    void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    void SkillSetting()
    {
        for(int i = 0; i < 2; i++)
        {
        PlayerPrefs.SetFloat($"HP{i}", Hp);
        PlayerPrefs.SetFloat($"BulletSpeed{i}", bulletSpeed);
        PlayerPrefs.SetFloat($"BulletReloadTime{i}", bulletReloadTime);
        PlayerPrefs.SetFloat($"BulletDmg{i}", bulletDmg);
        PlayerPrefs.SetInt($"BulletCount{i}", 3);
        }
    }


    void CardSetting()
    {
         CardChoice.SetBool("isChoice", isChoice);
        var card = new HashSet<string>();//중복되는 스킬 여부 확인
        int i = 0;
        while (card.Count < 5)
        {
            int rnd = Random.Range(0, skills.Length);
            //SpriteRenderer cardSprite = skillCard[i].gameObject.GetComponent<SpriteRenderer>();
            if (!card.Contains(skills[rnd].name))
            {
                //cardSprite.sprite = CardImage[rnd];
                //card.Add(i, cardSprite.sprite.name);
                GameObject CardSkill = Instantiate(skills[rnd]);
                //Debug.Log(skillCard[i].name);
                //CardSkill.transform.SetParent(skillCard[i], true);
                CardSkill.transform.parent = skillCard[i];
                CardSkill.transform.localPosition = Vector3.zero;
                CardSkill.transform.localRotation = Quaternion.identity;
                card.Add(skills[rnd].name);
                i++;
            }
        }
        Invoke("CardPlace", 1.3f);
    }
}
