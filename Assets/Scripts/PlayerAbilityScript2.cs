using UnityEngine;

public class PlayerAbilityScript2 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float dashForce = 10f;
    public int maxActivations = 3;
    private int activationsRemaining;

    void Start()
    {
        activationsRemaining = maxActivations;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && activationsRemaining > 0)
        {
            Dash(); // Call the Dash method when Y key is pressed and activations remaining
        }
    }

    public void Dash()
    {
        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = dashDirection * dashForce;
        activationsRemaining--;

        if (activationsRemaining == 0)
        {
            // Disable the ability or provide feedback that it's not available anymore
            Debug.Log("No more activations remaining for Dash ability.");
        }
    }
}
