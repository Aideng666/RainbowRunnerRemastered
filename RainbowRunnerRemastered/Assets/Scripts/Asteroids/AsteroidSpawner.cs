using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] PlatformSpawner platformSpawner;
    [SerializeField] PlayerMovement player;
    [SerializeField] Vector2 asteroidSpawnDelayRange = new Vector2(7.5f, 10f);
    [SerializeField] float minAsteroidSpawnDistance = 15;

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

        //Check if the selected platform is too close to the player and reselects a platform if it is
        if (Vector3.Distance(selectedSpawnPlatform.position, player.transform.position) < minAsteroidSpawnDistance)
        {
            SpawnAsteroid();

            return;
        }

        Vector3 spawnPos = Vector3.zero;

        //Spawns the asteroid in the correct location based on the platform type
        if (selectedSpawnPlatform.CompareTag("Platform"))
        {
            spawnPos = selectedSpawnPlatform.position + (Vector3.forward * Random.Range(-4f, 4f)) + (Vector3.up * 2) + (Vector3.forward * 15);
        }
        else if (selectedSpawnPlatform.CompareTag("Wall"))
        {
            int randomSide = Random.Range(0, 2);

            //Left Side
            if (randomSide == 0)
            {
                spawnPos = selectedSpawnPlatform.position + (Vector3.up * Random.Range(-4f, 4f)) + (Vector3.left * 2) + (Vector3.forward * 15);
            }
            //Right Side
            else
            {
                spawnPos = selectedSpawnPlatform.position + (Vector3.up * Random.Range(-4f, 4f)) + (Vector3.right * 2) + (Vector3.forward * 15);
            }
        }

        AsteroidPool.Instance.SpawnAsteroid(spawnPos);
    }
}
