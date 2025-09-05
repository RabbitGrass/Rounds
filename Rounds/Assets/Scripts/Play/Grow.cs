using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    Vector3 Scale;
    int growScale;
    // Start is called before the first frame update
    void Start()
    {
        Scale = transform.localScale;
        //growScale = PlayerPrefs("")
    }

    private void OnEnable()
    {
        transform.localScale = Scale;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 grow = transform.localScale;

        //grow.x += 
    }
}
