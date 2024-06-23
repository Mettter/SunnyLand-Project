using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    public float jumpForce;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointB.transform;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveEnemy();
        Jump();
    }

    private void MoveEnemy()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            Flip();
            if (currentPoint == PointB.transform)
            {
                currentPoint = PointA.transform;
            }
            else
            {
                currentPoint = PointB.transform;
            }
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
