using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float deg;
    public Transform target;
    private void Awake()
    {

    }

    private void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        Vector2 targetPos = target.position;
        float rad = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x);
        deg = Mathf.Rad2Deg * rad;

        Vector2 arrow = (targetPos - (Vector2)transform.position).normalized;
        transform.rotation = Quaternion.Euler(arrow);
    }
}
