using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction color1Action;
    InputAction color2Action;
    InputAction color3Action;

    public static InputManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        color1Action = playerInput.actions["Color1"];
        color2Action = playerInput.actions["Color2"];
        color3Action = playerInput.actions["Color3"];
    }

    public Vector2 GetMoveInput()
    {
        return moveAction.ReadValue<Vector2>();
    }

    public bool GetJumpInput()
    {
        return jumpAction.triggered;
    }

    public bool GetColor1Input()
    {
        return color1Action.triggered;
    }

    public bool GetColor2Input()
    {
        return color2Action.triggered;
    }

    public bool GetColor3Input()
    {
        return color3Action.triggered;
    }
}
