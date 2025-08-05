using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private bool jumpAble = false;
    Vector2 direction;

    private void FixedUpdate()
    {
        Debug.Log(jumpAble);
        Vector2 vector = transform.position;
        Movement(vector);
    }

    private void Movement(Vector2 vector)
    {
        if (Input.GetKey(KeyCode.D))
        {
            vector.x += speed;
            rb.AddForce(vector);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            vector.x -= speed;
            rb.AddForce(vector);
        }

        if (jumpAble && Input.GetKey(KeyCode.Space))
        {
            direction = direction.normalized * 200;
            rb.AddForce(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = collision.gameObject.transform.position - transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        jumpAble = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        jumpAble = false;
    }
}
