using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private float dmg;
    public Poison poison;
    void Start()
    {
        int pos = gameObject.GetComponent<Bullet>().pos;
        dmg = pos;

        dmg *= 0.25f;
        Debug.Log("Æ÷ÀÌÁð");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!poison.enabled)
            return;
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (collision.gameObject.activeSelf)
        {
            HpController hp = collision.gameObject.GetComponent<HpController>();
            hp.StartCoroutine(hp.Poison(dmg));
        }

    }
}
