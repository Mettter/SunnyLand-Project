using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMove : MonoBehaviour
{
    private float horizontal;

    private float speedB = 3.5f;

    private float jumpingPowerB = 32f;

    private bool isFacingRightB = true;

    private bool isJumpingB;

    private float coyoteTimeB = 0.2f;

    private float coyoteTimeCounterB;

    private float jumpBufferTimeB = 0.2f;

    private float jumpBufferCounterB;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform groundCheckB;

    [SerializeField] private LayerMask groundLayerB;

    private void UpdateB()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGroundedB())
        {
            coyoteTimeCounterB = coyoteTimeB;
        }
        else
        {
            coyoteTimeCounterB -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            jumpBufferCounterB = jumpBufferTimeB;
        }
        else
        {
            jumpBufferCounterB -= Time.deltaTime;
        }

        if (coyoteTimeCounterB > 0f && jumpBufferCounterB > 0f && !isJumpingB)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPowerB);

            jumpBufferCounterB = 0f;

            StartCoroutine(JumpCooldownB());
        }

        if (Input.GetKeyUp(KeyCode.R) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounterB = 0f;
        }

        FlipB();
    }

    private void FixedUpdateB()
    {
        rb.velocity = new Vector2(horizontal * speedB, rb.velocity.y);
    }

    private bool IsGroundedB()
    {
        return Physics2D.OverlapCircle(groundCheckB.position, 0.2f, groundLayerB);
    }

    private void FlipB()
    {
        if (isFacingRightB && horizontal < 0f || !isFacingRightB && horizontal > 0f)
        {
            Vector3 localScaleB = transform.localScale;
            isFacingRightB = !isFacingRightB;
            localScaleB.x *= -1f;
            transform.localScale = localScaleB;
        }
    }

    private IEnumerator JumpCooldownB()
    {
        isJumpingB = true;
        yield return new WaitForSeconds(0.4f);
        isJumpingB = false;
    }
}
