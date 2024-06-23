using UnityEngine;

public class PlayerAbilityScript1 : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool activated = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && !activated)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (!activated && rb != null)
        {
            activated = true;
            rb.gravityScale = 1f;
            Invoke("ResetGravityScale", 3f); // Reset gravity scale after 3 seconds
        }
    }

    private void ResetGravityScale()
    {
        rb.gravityScale = 3f; // Assuming you want to set gravity back to 0 after the ability duration
    }
}
