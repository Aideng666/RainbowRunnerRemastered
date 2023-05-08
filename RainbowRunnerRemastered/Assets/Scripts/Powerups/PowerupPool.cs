using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPool : MonoBehaviour
{
    [SerializeField] PowerupSpawner spawner;
    GameObject[] powerups;

    List<Powerup> availablePowerups;

    public static PowerupPool Instance { get; private set; }

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
        availablePowerups = new List<Powerup>();
    }

    GameObject SpawnNewPowerup(Powerups type, Vector3 pos)
    {
        powerups = spawner.possiblePowerups;

        foreach (GameObject powerup in powerups)
        {
            if (powerup.GetComponent<Powerup>().PowerupType == type)
            {
                GameObject spawnedPowerup = Instantiate(powerup, transform);

                spawnedPowerup.transform.position = pos;

                return spawnedPowerup;
            }
        }

        return null;
    }

    public GameObject SpawnPowerup(Powerups type, Vector3 pos)
    {
        if (availablePowerups.Count == 0)
        {
            return SpawnNewPowerup(type, pos);
        }
        else
        {
            foreach (Powerup powerup in availablePowerups)
            {
                if (powerup.PowerupType == type)
                {
                    Powerup spawnedPowerup = powerup;

                    availablePowerups.Remove(powerup);

                    spawnedPowerup.transform.position = pos;
                    spawnedPowerup.gameObject.SetActive(true);

                    return spawnedPowerup.gameObject;
                }
            }
        }

        return SpawnNewPowerup(type, pos);
    }

    public void AddPowerupToPool(GameObject powerup)
    {
        availablePowerups.Add(powerup.GetComponent<Powerup>());

        powerup.SetActive(false);
    }
}
