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

    CharacterController controller;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //Applies gravity if not grounded
        if (!controller.isGrounded)
        {
            ApplyGravity();
        }

        //Checks for jumping
        if (InputManager.Instance.GetJumpInput() && controller.isGrounded)
        {
            StartCoroutine(Jump());
        }

        //Lose condition resets the scene
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("Main");
        }

        //Sets the head aim target to the closest platform in front of them
        GameObject closestPlatform = null;

        foreach (GameObject platform in PlatformSpawner.Instance.activePlatforms)
        {
            if (closestPlatform == null && platform.transform.position.z > transform.position.z)
            {
                closestPlatform = platform;

                continue;
            }
            else if (closestPlatform == null)
            {
                continue;
            }

            if (Vector3.Distance(platform.transform.position, transform.position) < Vector3.Distance(closestPlatform.transform.position, transform.position) && platform.transform.position.z > transform.position.z)
            {
                closestPlatform = platform;
            }
        }

        if (closestPlatform != null)
        {
            SetHeadTargetPos(closestPlatform.transform.position);
        }
    }

    //Moves the player based on the Move input action in the InputManager
    void Move()
    {
        Vector2 moveInput = InputManager.Instance.GetMoveInput();

        Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y) * movementSpeed * Time.deltaTime;

        controller.Move(moveVector);
    }

    //Applies an upward jumping motion to the player that decreases over time to simulate a jump
    IEnumerator Jump()
    {
        float elaspedTime = 0;
        float currentJumpMovement = jumpForce;
        animator.SetTrigger("Jump");

        while(elaspedTime < jumpTime)
        {
            controller.Move(Vector3.up * Time.deltaTime * currentJumpMovement);

            currentJumpMovement *= 1 - Time.deltaTime;

            elaspedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    //Applies gravity to the player
    void ApplyGravity()
    {
        controller.Move(Vector3.down * Time.deltaTime * gravity);
    }

    //Sets the target for what the character is currently looking at
    public void SetHeadTargetPos(Vector3 position)
    {
        headAimTarget.transform.position = position;
    }
}
