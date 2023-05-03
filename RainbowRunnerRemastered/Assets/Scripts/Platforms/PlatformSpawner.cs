using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] float zSpawnPos = 20;
    [SerializeField] Vector2 minMaxXPositions = new Vector2(-10, 10);

    public List<GameObject> activePlatforms { get; private set; } = new List<GameObject>();

    float elaspedDelayTime = 0;
    float previousXPos = 0;
    bool platformsSpawned = false;

    public static PlatformSpawner Instance { get; private set; }

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
        platformsSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameStarted && !platformsSpawned)
        { 
            float xPos = Random.Range(Mathf.Max(previousXPos - 5, minMaxXPositions.x), Mathf.Min(previousXPos + 5, minMaxXPositions.y));
            GameObject platform = PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 10));
            activePlatforms.Add(platform);
            previousXPos = xPos;

            xPos = Random.Range(Mathf.Max(previousXPos - 5, minMaxXPositions.x), Mathf.Min(previousXPos + 5, minMaxXPositions.y));
            platform = PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 25));
            activePlatforms.Add(platform);
            previousXPos = xPos;

            xPos = Random.Range(Mathf.Max(previousXPos - 5, minMaxXPositions.x), Mathf.Min(previousXPos + 5, minMaxXPositions.y));
            platform = PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 40));
            activePlatforms.Add(platform);
            previousXPos = xPos;

            xPos = Random.Range(Mathf.Max(previousXPos - 5, minMaxXPositions.x), Mathf.Min(previousXPos + 5, minMaxXPositions.y));
            platform = PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, 55));
            activePlatforms.Add(platform);
            previousXPos = xPos;

            platformsSpawned = true;
        }

        if (GameManager.Instance.gameStarted)
        {
            if (elaspedDelayTime > spawnDelay)
            {
                int platformChoice = Random.Range(0, 2); // chooses between wall or platform
                float xPos = Random.Range(Mathf.Max(previousXPos - 5, minMaxXPositions.x), Mathf.Min(previousXPos + 5, minMaxXPositions.y));

                if (platformChoice == 0)
                {
                    activePlatforms.Add(PlatformPool.Instance.SpawnPlatform(new Vector3(xPos, 0, zSpawnPos)));
                }
                else if (platformChoice == 1)
                {
                    activePlatforms.Add(PlatformPool.Instance.SpawnWall(new Vector3(xPos, 4, zSpawnPos)));
                }

                previousXPos = xPos;
                elaspedDelayTime = 0;

                return;
            }

            elaspedDelayTime += Time.deltaTime;
        }
    }

    public void DeactivatePlatform(GameObject platform)
    {
        activePlatforms.Remove(platform);
        PlatformPool.Instance.AddPlatformToPool(platform);
    }
}
