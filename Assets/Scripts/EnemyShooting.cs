using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject playerObject; // Reference to the player object
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float projectileSpeed = 5f; // Speed of the projectile
    public float shootingInterval = 2f; // Time interval between each shot

    void Start()
    {
        if (playerObject == null)
        {
            Debug.LogError("PlayerObject is not assigned. Please assign PlayerObject in the inspector.");
            return;
        }

        if (projectilePrefab == null)
        {
            Debug.LogError("ProjectilePrefab is not assigned. Please assign ProjectilePrefab in the inspector.");
            return;
        }

        // Start shooting at regular intervals
        InvokeRepeating("ShootAtPlayer", 0f, shootingInterval);
    }

    void ShootAtPlayer()
    {
        if (playerObject != null && projectilePrefab != null)
        {
            // Calculate direction to the player
            Vector3 direction = (playerObject.transform.position - transform.position).normalized;

            // Instantiate the projectile at the enemy's position
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Check if the instantiated projectile has a Rigidbody2D component
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }
            else
            {
                Debug.LogError("Projectile does not have a Rigidbody2D component.");
            }
        }
    }
}