using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] PlatformSpawner platformSpawner;
    [SerializeField] float powerupSpawnTimer = 15f;

    float elaspedRunTime = 0;

    public GameObject[] possiblePowerups { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        elaspedRunTime = 0;

        possiblePowerups = GetAllPowerups();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            if (elaspedRunTime >= powerupSpawnTimer)
            {
                SpawnPowerup();

                elaspedRunTime = 0;
            }

            elaspedRunTime += Time.deltaTime;
        }
    }

    //Finds the current platform group that was spawned and randomly selects one of the platforms/walls to spawn a powerup on
    void SpawnPowerup()
    {
        GameObject currentPlatformGroup = platformSpawner.CurrentGroup;

        int randomChild = Random.Range(0, currentPlatformGroup.transform.childCount); // Random Platform
        int powerupChoice = Random.Range(0, possiblePowerups.Length); // Random Powerup

        Transform selectedSpawnPlatform = currentPlatformGroup.transform.GetChild(randomChild);

        Vector3 spawnPos = Vector3.zero;

        //Spawns the powerup in the correct location based on the platform type
        if (selectedSpawnPlatform.CompareTag("Platform"))
        {
            spawnPos = selectedSpawnPlatform.position + (Vector3.forward * Random.Range(-4f, 4f)) + (Vector3.up * 2);
        }
        else if (selectedSpawnPlatform.CompareTag("Wall"))
        {
            int randomSide = Random.Range(0, 2);

            //Left Side
            if (randomSide == 0)
            {
                spawnPos = selectedSpawnPlatform.position + (Vector3.up * Random.Range(-4f, 4f)) + (Vector3.left * 2);
            }
            //Right Side
            else
            {
                spawnPos = selectedSpawnPlatform.position + (Vector3.up * Random.Range(-4f, 4f)) + (Vector3.right * 2);
            }
        }

        PowerupPool.Instance.SpawnPowerup((Powerups)powerupChoice, spawnPos);
    }

    public GameObject[] GetAllPowerups()
    {
        return Resources.LoadAll<GameObject>("Powerups");
    }
}
