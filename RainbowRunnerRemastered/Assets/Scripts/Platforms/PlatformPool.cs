using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject wallPrefab;
    Queue<GameObject> availablePlatforms;
    Queue<GameObject> availableWalls;
    int numPlatforms = 12;

    public static PlatformPool Instance { get; private set; }

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
        availablePlatforms = new Queue<GameObject>();
        availableWalls = new Queue<GameObject>();

        CreatePlatformPool();
        CreateWallPool();
    }

    void CreatePlatformPool()
    {
        for (int i = 0; i < numPlatforms; i++)
        {
            GameObject platform = Instantiate(platformPrefab, transform);

            availablePlatforms.Enqueue(platform);

            platform.SetActive(false);
        }
    }

    void CreateWallPool()
    {
        for (int i = 0; i < numPlatforms; i++)
        {
            GameObject wall = Instantiate(wallPrefab, transform);

            availableWalls.Enqueue(wall);

            wall.SetActive(false);
        }
    }

    public GameObject SpawnPlatform(Vector3 pos)
    {
        if (availablePlatforms.Count == 0)
        {
            CreatePlatformPool();
        }

        GameObject platform = availablePlatforms.Dequeue();

        platform.transform.position = pos;
        platform.SetActive(true);

        return platform;
    }

    public GameObject SpawnWall(Vector3 pos)
    {
        if (availableWalls.Count == 0)
        {
            CreateWallPool();
        }

        GameObject wall = availableWalls.Dequeue();

        wall.transform.position = pos;
        wall.SetActive(true);

        return wall;
    }

    public void AddPlatformToPool(GameObject platform)
    {
        availablePlatforms.Enqueue(platform);

        platform.SetActive(false);
    }

    public void AddWallToPool(GameObject wall)
    {
        availableWalls.Enqueue(wall);

        wall.SetActive(false);
    }
}
