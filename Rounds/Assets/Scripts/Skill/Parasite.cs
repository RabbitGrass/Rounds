using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
    private float dmg;
    public GameObject player;
    void Start()
    {
        dmg = PlayerPrefs.GetInt("Parasite");

        dmg *= 0.25f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (collision.gameObject.activeSelf)
        {
            HpController hp = collision.gameObject.GetComponent<HpController>();
            hp.StartCoroutine(hp.Parasite(dmg, player));
        }
    }
}
