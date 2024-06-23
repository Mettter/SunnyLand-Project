using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 0.1f; // Time in seconds before the projectile is destroyed
    public float rotationSpeed = 360f; // Speed of the rotation in degrees per second

    void Start()
    {
        // Schedule the destruction of the projectile after the specified lifetime
        DestroyProjectile();
    }

    void Update()
    {
        // Rotate the projectile continuously
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the projectile upon collision
        Destroy(gameObject);
    }

    void DestroyProjectile()
    {
        // Destroy the projectile
        Destroy(gameObject, lifetime);
    }
}