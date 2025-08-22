using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private float dmg;
    void Start()
    {
        dmg = PlayerPrefs.GetInt("Poison");

        dmg *= 0.25f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (collision.gameObject.activeSelf)
        {
            HpController hp = collision.gameObject.GetComponent<HpController>();
            hp.StartCoroutine(hp.Poison(dmg));
        }

    }
}
