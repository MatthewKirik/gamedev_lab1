using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;  // Adjust this to set the character's movement speed.
    public float jumpForce = 10f; // Adjust this to set the jump force.
    public Transform groundCheck; // A reference to an empty GameObject placed at the character's feet.
    public LayerMask groundLayer; // Define the ground layer for the grounded check.
    public float obstacleDetectionDistance = 0.2f; // Distance to check for obstacles.

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Grounded check using a small circle overlap at the character's feet.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Get input from the player (e.g., arrow keys).
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the velocity based on the input and speed.
        Vector2 velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // Set the character's velocity to move it.
        rb.velocity = velocity;

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Apply an upward force for jumping when grounded.
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Check for obstacles in the direction of movement.
        if (CheckForObstacles(horizontalInput))
        {
            rb.velocity = new Vector2(0f, rb.velocity.y); // Stop movement if an obstacle is detected.
        }
    }

    bool CheckForObstacles(float direction)
    {
        Vector2 checkPosition = transform.position.To2D() + Vector2.right * direction * obstacleDetectionDistance;
        RaycastHit2D hit = Physics2D.Raycast(Vector2.zero, checkPosition, obstacleDetectionDistance, groundLayer);

        return hit.collider != null; // Returns true if any obstacle is detected.
    }
}