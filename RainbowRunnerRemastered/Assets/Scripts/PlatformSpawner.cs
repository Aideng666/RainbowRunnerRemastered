using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] float zSpawnPos = 20;

    float elaspedDelayTime = 0;
    float previousXPos = 0;
    bool platformsSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        platformsSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameStarted && !platformsSpawned)
        { 
            float xPos = Random.Range(previousXPos - 5, previousXPos + 5);
            PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 10));
            previousXPos = xPos;

            xPos = Random.Range(previousXPos - 5, previousXPos + 5);
            PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 25));
            previousXPos = xPos;

            xPos = Random.Range(previousXPos - 5, previousXPos + 5);
            PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 40));
            previousXPos = xPos;

            xPos = Random.Range(previousXPos - 5, previousXPos + 5);
            PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 55));
            previousXPos = xPos;

            platformsSpawned = true;
        }

        if (GameManager.Instance.gameStarted)
        {
            if (elaspedDelayTime > spawnDelay)
            {
                float xPos = Random.Range(previousXPos - 5, previousXPos + 5);

                PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, zSpawnPos));

                previousXPos = xPos;
                elaspedDelayTime = 0;

                return;
            }

            elaspedDelayTime += Time.deltaTime;
        }
    }
}
