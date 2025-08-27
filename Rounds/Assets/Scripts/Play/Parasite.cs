using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
    private float dmg;
    public GameObject player;
    public Parasite parasite;
    void Start()
    {
        int par = gameObject.GetComponent<Bullet>().par;
        dmg = par;

        dmg *= 0.25f;
        Debug.Log("패러사이트");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!parasite.enabled)
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (collision.gameObject.activeSelf)
        {
            HpController hp = collision.gameObject.GetComponent<HpController>();
            Debug.Log(gameObject.name);
            hp.StartCoroutine(hp.Parasite(dmg, player));
        }
    }
}
