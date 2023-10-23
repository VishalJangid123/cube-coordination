using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    public bool isControlled = false;
    public bool playerReached = false;
    public float moveSpeed = 5.0f;

    public Vector2 inputs;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (GameManager.Instance.gameOver)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        HandleInput();
    }

    protected virtual void HandleMovement(Vector2 move) { }

    public void TakeControl()
    {
        isControlled = true;
    }

    public void ReleaseControl()
    {
        isControlled = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Cone") && (this is Player || this is PlayerMirror))
        {
            FindObjectOfType<PlayerSwitchController>().PlayerDie();
            GameManager.Instance.GameOver();
        }
    }

    protected void EndGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the scene
    }


  
    void HandleInput()

    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        inputs = new Vector2(horizontalInput, verticalInput);
        Vector2 inputDirection = inputs.normalized;
        //Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed;

        Vector3 rot = Vector3.zero;
        Vector3 movement = Vector2.zero;

        if (horizontalInput > 0)
        {
            rot = new Vector3(0, 0, -90);
            movement = new Vector2(horizontalInput, 0);

        }
        else if (horizontalInput < 0)
        {
            rot = new Vector3(0, 0, 90);
            movement = new Vector2(horizontalInput, 0);
        }

        if (verticalInput > 0)
        {
            rot = new Vector3(0, 0, 0);
            movement = new Vector2(0, verticalInput);

        }
        else if (verticalInput < 0)
        {
            rot = new Vector3(0, 0, 180);
            movement = new Vector2(0, verticalInput);
        }

        if (isControlled)
        {


            //float angle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;
            //rb.rotation = 90 * verticalInput;
            //rb.MoveRotation(Quaternion.LookRotation(movement));


            transform.eulerAngles = rot;
            rb.velocity = movement.normalized * moveSpeed;

            float flipAngle = Vector2.SignedAngle(Vector2.up, movement);
        }

        else
        {
            //rb.velocity = -movement;
            float flipAngle = Vector2.SignedAngle(Vector2.up, -movement);
            Quaternion rotation = Quaternion.Euler(0f, 0f, flipAngle);
            transform.eulerAngles = rotation.eulerAngles;
            rb.velocity = -movement.normalized * moveSpeed;
        }

    }

    public void MoveToDestination(Vector2 dest)
    {
        rb.simulated = false;
        transform.position = dest;
        rb.simulated = true;
    }
}
