using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] PlatformSpawner platformSpawner;
    [SerializeField] Vector2 asteroidSpawnDelayRange = new Vector2(7.5f, 10f);

    float elaspedDelayTime = 0;
    float chosenSpawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        elaspedDelayTime = 0;

        chosenSpawnDelay = Random.Range(asteroidSpawnDelayRange.x, asteroidSpawnDelayRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        //Spawns an asteroid after a delay
        if (GameManager.Instance.gameStarted)
        {
            if (elaspedDelayTime >= chosenSpawnDelay)
            {
                SpawnAsteroid();

                elaspedDelayTime = 0;
                chosenSpawnDelay = Random.Range(asteroidSpawnDelayRange.x, asteroidSpawnDelayRange.y);
            }

            elaspedDelayTime += Time.deltaTime;
        }
    }

    void SpawnAsteroid()
    {
        //Selects a random platform from the current group
        GameObject currentPlatformGroup = platformSpawner.CurrentGroup;

        int randomChild = Random.Range(0, currentPlatformGroup.transform.childCount);

        Transform selectedSpawnPlatform = currentPlatformGroup.transform.GetChild(randomChild);

        Vector3 spawnPos = Vector3.zero;

        //Spawns the asteroid in the correct location based on the platform type
        if (selectedSpawnPlatform.CompareTag("Platform"))
        {
            spawnPos = selectedSpawnPlatform.position + (Vector3.forward * Random.Range(-4f, 4f) * 5) + (Vector3.up * 2);
        }
        else if (selectedSpawnPlatform.CompareTag("Wall"))
        {
            int randomSide = Random.Range(0, 2);

            //Left Side
            if (randomSide == 0)
            {
                spawnPos = selectedSpawnPlatform.position + (Vector3.up * Random.Range(-4f, 4f)) + (Vector3.left * 2) + (Vector3.forward * 5);
            }
            //Right Side
            else
            {
                spawnPos = selectedSpawnPlatform.position + (Vector3.up * Random.Range(-4f, 4f)) + (Vector3.right * 2) + (Vector3.forward * 5);
            }
        }

        AsteroidPool.Instance.SpawnAsteroid(spawnPos);
    }
}
