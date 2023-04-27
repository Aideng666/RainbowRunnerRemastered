using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameStarted { get; private set; } = false;

    public static GameManager Instance { get; private set; }

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
        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && InputManager.Instance.GetJumpInput())
        {
            gameStarted = true;
        }
    }
}
