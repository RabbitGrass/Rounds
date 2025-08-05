using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private bool jumpAble = false;
    Vector2 direction;

    public Transform groundCheckTransform;
    private bool grounded;

    private void FixedUpdate()
    {
        Vector2 vector = transform.position;
        Movement(vector);
        UpdateGroundedStatus();
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
            if (grounded)
            {
                vector.y += 150;
            }
            else if (!grounded)
            {
                vector.y += 130;
                rb.AddForce(direction);
            }
            rb.AddForce(vector);
        }
    }

    private void UpdateGroundedStatus()
    {
        grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.001f);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpAble = true;
        Debug.Log("jump : " + jumpAble);
        Debug.Log("ground : " + grounded);
        if (!grounded)
        {
            direction = collision.gameObject.transform.position - transform.position;
            direction = direction.normalized * (-100);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (grounded && !jumpAble)
        {
            jumpAble = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
         jumpAble = false;
         Debug.Log("jump : " + jumpAble);
    }
}
