using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckFix : MonoBehaviour
{
    public GameObject objectB;

    void Start()
    {
        if (objectB == null)
        {
            Debug.LogError("ObjectB is not assigned. Please assign ObjectB in the inspector.");
            return;
        }

        // Start teleporting the object infinitely
        StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
        while (true)
        {
            // Teleport ObjectA to 0.5 units lower than ObjectB's position
            Vector3 newPosition = objectB.transform.position;
            newPosition.y -= 0.5f;
            transform.position = newPosition;

            // Wait for a frame before the next teleport to avoid freezing the game
            yield return null;
        }
    }
}

