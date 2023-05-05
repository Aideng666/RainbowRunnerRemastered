using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_DoubleScore : MonoBehaviour, IPowerUp
{
    PlayerScore score;
    float elaspedPowerupTime = 0;
    bool powerupActivated = false;

    private void Start()
    {
        score = FindObjectOfType<PlayerScore>();

        elaspedPowerupTime = 0;
        powerupActivated = false;
    }

    private void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            transform.position += Vector3.back * GameManager.Instance.PlatformMoveSpeed * Time.deltaTime;
        }

        if (powerupActivated)
        {
            if (elaspedPowerupTime >= GameManager.Instance.PowerupDuration)
            {
                RemovePowerup();
            }

            elaspedPowerupTime += Time.deltaTime;
        }
    }

    public void ApplyPowerup()
    {
        powerupActivated = true;
        score.ScoreMultiplier *= 2;

        print("Powerup applied");
    }

    public void RemovePowerup()
    {
        powerupActivated = false;
        score.ScoreMultiplier /= 2;

        print("Powerup removed");

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerup();
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
