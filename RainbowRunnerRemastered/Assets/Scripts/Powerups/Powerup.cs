using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] Powerups powerupType;

    protected float elaspedPowerupTime = 0;
    protected bool powerupActivated = false;

    public Powerups PowerupType { get { return powerupType; } }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        elaspedPowerupTime = 0;
        powerupActivated = false;
    }

    // Update is called once per frame
    void Update()
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

    protected virtual void ApplyPowerup()
    {
        powerupActivated = true;
    }

    protected virtual void RemovePowerup()
    {
        powerupActivated = false;

        PowerupPool.Instance.AddPowerupToPool(gameObject);
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

public enum Powerups
{
    DoublePoints,
    SpaceWalker
}

