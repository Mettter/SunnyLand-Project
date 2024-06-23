using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Transform[] targetObjects; // Array of 4 target objects
   // Prefab for the bullet to be spawned
    public GameObject[] wallObjects1; // First array of wall objects to be destroyed
    public GameObject[] wallObjects2; // Second array of wall objects to be destroyed
    public float speed = 5f; // Speed of the boss movement
    public int maxBullets = 4; // Maximum number of bullets to spawn

    private Transform currentTarget;
    private int previousTargetIndex = -1;
    private int bulletsSpawned = 0; // Counter for the number of bullets spawned

    private void Start()
    {
        // Set an initial random target
        SetRandomTarget();
    }

    private void Update()
    {
        // Move towards the current target
        MoveToTarget();

        // Check if the boss has reached the target
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            // Check if we can still spawn bullets

            // Set a new random target
            SetRandomTarget();
        }
    }

    private void MoveToTarget()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void SetRandomTarget()
    {
        int newTargetIndex;

        // Ensure the new target is different from the previous one
        do
        {
            newTargetIndex = Random.Range(0, targetObjects.Length);
        } while (newTargetIndex == previousTargetIndex);

        previousTargetIndex = newTargetIndex;
        currentTarget = targetObjects[newTargetIndex];
    }

    private void OnDisable()
    {
        DestroyWallObjects();
    }

    private void OnDestroy()
    {
        DestroyWallObjects();
    }

    private void DestroyWallObjects()
    {
        // Destroy the first set of wall objects
        foreach (GameObject wallObject in wallObjects1)
        {
            if (wallObject != null)
            {
                Destroy(wallObject);
            }
        }

        // Destroy the second set of wall objects
        foreach (GameObject wallObject in wallObjects2)
        {
            if (wallObject != null)
            {
                Destroy(wallObject);
            }
        }
    }
}
