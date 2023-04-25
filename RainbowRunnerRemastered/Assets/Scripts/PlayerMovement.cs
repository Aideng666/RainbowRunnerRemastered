using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float jumpTime = 0.5f;
    [SerializeField] float gravity = 9.81f;

    CharacterController controller;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        animator.SetBool("Running", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.isGrounded)
        {
            ApplyGravity();
        }

        Move();

        if (InputManager.Instance.GetJumpInput() && controller.isGrounded)
        {
            StartCoroutine(Jump());
        }
    }

    void Move()
    {
        Vector2 moveInput = InputManager.Instance.GetMoveInput();

        Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y) * movementSpeed * Time.deltaTime;

        controller.Move(moveVector);
    }

    IEnumerator Jump()
    {
        float elaspedTime = 0;
        animator.SetTrigger("Jump");

        while(elaspedTime < jumpTime)
        {
            controller.Move(Vector3.up * Time.deltaTime * jumpForce);

            elaspedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    void ApplyGravity()
    {
        controller.Move(Vector3.down * Time.deltaTime * gravity);
    }
}
