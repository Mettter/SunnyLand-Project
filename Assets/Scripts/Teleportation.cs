using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject targetObject; // Reference to the target object to be teleported
    public Transform destination; // Destination transform for teleportation

    void Update()
    {
        // Example: Call TeleportTarget every frame in Update
        TeleportTarget();
    }

    // Method to teleport the target object to the specified destination
    public void TeleportTarget()
    {
        if (targetObject != null && destination != null)
        {
            targetObject.transform.position = destination.position;
            // Add particle spawning or other effects here if needed
        }
        else
        {
            Debug.LogWarning("Target object or destination is not set for teleportation.");
        }
    }
}
