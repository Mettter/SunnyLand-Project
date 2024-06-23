using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControle : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointB.transform;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            flip();
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

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
