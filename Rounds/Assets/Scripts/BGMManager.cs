using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 넘어가도 안 없어짐
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }
}
