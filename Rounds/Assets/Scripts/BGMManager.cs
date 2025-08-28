using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� �Ѿ�� �� ������
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
        }
    }
}
