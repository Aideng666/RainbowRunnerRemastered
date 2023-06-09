using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] float platformMoveSpeed = 10;
    [SerializeField] float asteroidMoveSpeed = 15;
    [SerializeField] float powerupDuration = 10;

    public bool gameStarted { get; private set; } = false;
    public float PlatformMoveSpeed { get { return platformMoveSpeed; } }
    public float AsteroidMoveSpeed { get { return asteroidMoveSpeed; } }
    public float PowerupDuration { get { return powerupDuration; } }

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
            playerAnimator.SetBool("Running", true);
        }
    }
}
