using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashControll : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    public float baseSpeed = 5f; // Initial speed
    public float speedIncrement = 1f; // Amount to increase speed every 0.5 seconds
    public float maxSpeed = 20f; // Maximum speed the object can reach
    public float baseScale = 1f; // Initial scale
    public float scaleIncrement = 0.1f; // Scale increment per speed increment
    public float maxScale = 3f; // Maximum scale the object can reach

    private float currentSpeed;
    private Rigidbody2D rb;
    private Transform currentPoint;
    private Vector3 initialScale;
    private bool isFacingRight = true; // Track the facing direction
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component
        currentSpeed = baseSpeed;
        currentPoint = PointB.transform;
        initialScale = transform.localScale; // Save the initial scale
        StartCoroutine(IncreaseSpeed());
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * currentSpeed, rb.velocity.y);

        if (Vector2.Distance(transform.position, currentPoint.position) < 1f) // Increase the distance check
        {
            if (currentPoint == PointB.transform)
            {
                currentPoint = PointA.transform;
            }
            else
            {
                currentPoint = PointB.transform;
            }
            Flip();
        }

        // Trigger the Run animation
        animator.SetBool("isRunning", rb.velocity.x != 0);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += speedIncrement;
                UpdateScale();
                Debug.Log("Speed: " + currentSpeed);
            }
        }
    }

    private void UpdateScale()
    {
        float scaleFactor = 1 + (currentSpeed - baseSpeed) / baseSpeed * scaleIncrement;
        Vector3 newScale = initialScale * scaleFactor;

        // Ensure the new scale does not exceed the maxScale
        if (newScale.x > maxScale || newScale.y > maxScale || newScale.z > maxScale)
        {
            newScale = new Vector3(maxScale, maxScale, maxScale);
        }

        transform.localScale = newScale;
        Debug.Log("Scale: " + transform.localScale);
    }
}
