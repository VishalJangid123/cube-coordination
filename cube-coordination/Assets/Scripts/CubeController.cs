using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject cubeToControl; // Reference to the cube controlled by the player
    public GameObject otherCube;     // Reference to the other cube

    float movementSpeed = 5f;

    private bool isControllingCubeA = true; // Flag to track which cube is currently controlled

    Rigidbody2D rb, otherrb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        otherrb = otherCube.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check for input to switch control between cubes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchControl();
        }

        // Player controls the currently selected cube
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //// Move the controlled cube
        //Vector3 moveDirection = new Vector3(moveHorizontal, moveVertical, 0);
        //rb.MovePosition(moveDirection * Time.deltaTime * movementSpeed);

        // Move the other cube in the opposite direction
        //Vector3 oppositeMoveDirection = new Vector3(-moveHorizontal, -moveVertical, 0);
        //otherrb.MovePosition (oppositeMoveDirection * Time.deltaTime * movementSpeed);
        HandleMovement();
    }

    void HandleMovement()
    {
        // A and D
        float horizontalAxisMovement = Input.GetAxis("Horizontal");
        // W and S
        float verticalAxisMovement = Input.GetAxis("Vertical");

        // Create a movement vector out of the axis of input
        Vector2 newMovementVector = Vector2.zero;// = new Vector2(horizontalAxisMovement, verticalAxisMovement);

        if (Input.GetKey(KeyCode.W))
        {
            newMovementVector = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            newMovementVector = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            newMovementVector = Vector2.left;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            newMovementVector = Vector2.right;

        }

        // Apply the movement vector direction with the speed and apply it instantly. If an axis is 0 there will be no movement
        rb.velocity = newMovementVector * movementSpeed;
        otherrb.velocity = -newMovementVector * movementSpeed;
    }

    private void SwitchControl()
    {
        // Switch control between cubes
        isControllingCubeA = !isControllingCubeA;

        // Disable the Rigidbody of the previously controlled cube
        //cubeToControl.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        // Enable the Rigidbody of the newly controlled cube
        otherCube.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        // Swap cube references
        GameObject temp = cubeToControl;
        cubeToControl = otherCube;
        otherCube = temp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        print(collision.gameObject.name);

        if (collision.gameObject.CompareTag("WinArea"))
        {
            print("win");
        }
    }
}
