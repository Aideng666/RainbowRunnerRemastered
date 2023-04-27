using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] float zSpawnPos = 20;
    [SerializeField] Vector2 minMaxXPositions = new Vector2(-10, 10);

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
        if (!GameManager.Instance.gameStarted && !platformsSpawned)
        { 
            float xPos = Random.Range(Mathf.Max(previousXPos - 5, minMaxXPositions.x), Mathf.Min(previousXPos + 5, minMaxXPositions.y));
            PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 10)).GetComponent<Platform>().SetStartingPlatform();
            previousXPos = xPos;

            xPos = Random.Range(Mathf.Max(previousXPos - 5, minMaxXPositions.x), Mathf.Min(previousXPos + 5, minMaxXPositions.y));
            PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 25)).GetComponent<Platform>().SetStartingPlatform();
            previousXPos = xPos;

            xPos = Random.Range(Mathf.Max(previousXPos - 5, minMaxXPositions.x), Mathf.Min(previousXPos + 5, minMaxXPositions.y));
            PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 40)).GetComponent<Platform>().SetStartingPlatform();
            previousXPos = xPos;

            xPos = Random.Range(Mathf.Max(previousXPos - 5, minMaxXPositions.x), Mathf.Min(previousXPos + 5, minMaxXPositions.y));
            PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 55)).GetComponent<Platform>().SetStartingPlatform();
            previousXPos = xPos;

            platformsSpawned = true;
        }

        if (GameManager.Instance.gameStarted)
        {
            if (elaspedDelayTime > spawnDelay)
            {
                float xPos = Random.Range(Mathf.Max(previousXPos - 5, minMaxXPositions.x), Mathf.Min(previousXPos + 5, minMaxXPositions.y));

                PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, zSpawnPos));

                previousXPos = xPos;
                elaspedDelayTime = 0;

                return;
            }

            elaspedDelayTime += Time.deltaTime;
        }
    }
}
