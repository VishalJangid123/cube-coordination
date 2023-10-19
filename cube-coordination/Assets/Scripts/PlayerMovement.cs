using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool canDoubleJump = true; // Enable double jump
    public float wallJumpForce = 7f; // Force when wall jumping
    public float wallSlideSpeed = 2f; // Speed when sliding down a wall

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private int facingDirection = 1; // 1 for right, -1 for left
    private int wallDirX; // Direction of the wall (1 for right, -1 for left)

    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer); // Adjust ground check position and layer mask

        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, 0.5f, wallLayer); // Adjust raycast distance and layer mask
        isWallSliding = isTouchingWall && !isGrounded;

        // Player movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip the character if moving left or right
        if (moveInput > 0 && facingDirection != 1)
        {
            Flip();
        }
        else if (moveInput < 0 && facingDirection != -1)
        {
            Flip();
        }

        // Jumping
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
            else if (isWallSliding) // Wall jump
            {
                rb.velocity = new Vector2(-wallDirX * wallJumpForce, jumpForce);
            }
        }

        // Wall slide
        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
}
