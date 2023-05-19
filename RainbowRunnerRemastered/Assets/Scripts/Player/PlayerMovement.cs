using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float jumpTime = 0.5f;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] Transform headAimTarget;

    Rigidbody body;
    Animator animator;

    bool isWallRunning;
    int wallRunSide; // 0 means player is on the left side of the wall, 1 means player is running on the right side of a wall
    bool isGrounded;
    bool gravityApplied;
    float currentGravity;

    public float MoveSpeed { get { return movementSpeed; } set { movementSpeed = value; } }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        isGrounded = true;

        currentGravity = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = Vector3.zero;

        if (GameManager.Instance.gameStarted)
        {
            Move();
        }

        //Applies gravity if not grounded
        if (!isGrounded && !isWallRunning)
        {
            ApplyGravity();
            gravityApplied = true;
        }
        else
        {
            gravityApplied = false;
        }

        //Checks for jumping
        if (InputManager.Instance.GetJumpInput())
        {
            if (isGrounded)
            {
                StartCoroutine(Jump());
            }

            if (isWallRunning)
            {
                StartCoroutine(JumpOffWall());
            }
        }

        //Lose condition resets the scene
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("Main");
        }

        //Sets the head aim target to the closest platform in front of them
        //GameObject closestPlatform = null;

        //foreach (GameObject platform in PlatformSpawner.Instance.activePlatforms)
        //{
        //    if (closestPlatform == null && platform.transform.position.z > transform.position.z)
        //    {
        //        closestPlatform = platform;

        //        continue;
        //    }
        //    else if (closestPlatform == null)
        //    {
        //        continue;
        //    }

        //    if (Vector3.Distance(platform.transform.position, transform.position) < Vector3.Distance(closestPlatform.transform.position, transform.position) && platform.transform.position.z > transform.position.z)
        //    {
        //        closestPlatform = platform;
        //    }
        //}

        //if (closestPlatform != null)
        //{
        //    SetHeadTargetPos(closestPlatform.transform.position);
        //}

        Mathf.Clamp(wallRunSide, 0, 1);

        if (gravityApplied)
        {
            currentGravity += 2 * Time.deltaTime;
        }
        else
        {
            currentGravity = gravity;
        }
    }

    //Moves the player based on the Move input action in the InputManager
    void Move()
    {
        Vector2 moveInput = InputManager.Instance.GetMoveInput();
        Vector3 moveVector = Vector3.zero;

        if (!isWallRunning)
        {
            moveVector = new Vector3(moveInput.x, body.velocity.y, body.velocity.z) * movementSpeed;
        }
        else
        {
            moveVector = new Vector3(body.velocity.x, moveInput.y, body.velocity.z) * movementSpeed;
        }

        body.velocity = moveVector;

        //Rotates the player based on movement
        if (body.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else if (body.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
        else if (body.velocity.x == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    //Applies an upward jumping motion to the player that decreases over time to simulate a jump
    IEnumerator Jump()
    {
        float elaspedTime = 0;
        isGrounded = false;
        isWallRunning = false;
        animator.SetTrigger("Jump");

        while (elaspedTime < jumpTime)
        {
            body.velocity = new Vector3(body.velocity.x, Mathf.Lerp(jumpForce, 0, elaspedTime / jumpTime), body.velocity.z);

            elaspedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    //Jumping off of a wall gives a little bit of sideways movement as well as upwards movement
    IEnumerator JumpOffWall()
    {
        float elaspedTime = 0;
        isGrounded = false;
        isWallRunning = false;
        animator.SetTrigger("Jump");

        while (elaspedTime < jumpTime)
        {
            if (wallRunSide == 0)
            {
                body.velocity = new Vector3(body.velocity.x + Mathf.Lerp(-movementSpeed, 0, elaspedTime / jumpTime), Mathf.Lerp(jumpForce, 0, elaspedTime / jumpTime), body.velocity.z);
            }
            else if (wallRunSide == 1)
            {
                body.velocity = new Vector3(body.velocity.x + Mathf.Lerp(movementSpeed, 0, elaspedTime / jumpTime), Mathf.Lerp(jumpForce, 0, elaspedTime / jumpTime), body.velocity.z);
            }

            elaspedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    //Applies gravity to the player
    void ApplyGravity()
    {
        body.velocity += Vector3.down * currentGravity;
    }

    //Sets the target for what the character is currently looking at
    public void SetHeadTargetPos(Vector3 position)
    {
        headAimTarget.transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Asteroid Collision
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            SceneManager.LoadScene("Main");
        }

        //Checks for grounded and wall running
        if (collision.contacts[0].normal.y > 0.4f && collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }

        if (collision.contacts[0].normal.x <= -0.4f && collision.gameObject.CompareTag("Wall"))
        {
            //Left side of wall
            isWallRunning = true;
            wallRunSide = 0;
        }
        else if (collision.contacts[0].normal.x >= 0.4f && collision.gameObject.CompareTag("Wall"))
        {
            //right side of wall
            isWallRunning = true;
            wallRunSide = 1;
        }
    }

    //Resets flags when leaving a collision
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        isWallRunning = false;
    }
}
