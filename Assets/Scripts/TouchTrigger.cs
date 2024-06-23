using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrigger : MonoBehaviour
{
    public GameObject objectB; // Reference to ObjectB

    void Start()
    {
        if (objectB == null)
        {
            Debug.LogError("ObjectB is not assigned. Please assign ObjectB in the inspector.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player (or any specific object you want)
        // Assuming the player has a tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            AddRigidbody2DWithGravity(objectB);
        }
    }

    void AddRigidbody2DWithGravity(GameObject obj)
    {
        // Check if the object already has a Rigidbody2D
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            // Add Rigidbody2D component
            rb = obj.AddComponent<Rigidbody2D>();
        }

        // Enable gravity on the Rigidbody2D
        rb.gravityScale = 1f;
    }
}

