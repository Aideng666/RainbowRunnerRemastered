using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;

    Queue<GameObject> availablePlatforms;
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

        CreatePool();
    }

    void CreatePool()
    {
        for (int i = 0; i < numPlatforms; i++)
        {
            GameObject platform = Instantiate(platformPrefab, transform);

            availablePlatforms.Enqueue(platform);

            platform.SetActive(false);
        }
    }

    public GameObject SpawnPlatform(Vector3 pos)
    {
        if (availablePlatforms.Count == 0)
        {
            CreatePool();
        }

        GameObject platform = availablePlatforms.Dequeue();

        platform.SetActive(true);
        platform.transform.position = pos;
        platform.transform.localScale = new Vector3(2, 1, 10);

        return platform;
    }

    public void AddPlatformToPool(GameObject platform)
    {
        availablePlatforms.Enqueue(platform);

        platform.SetActive(false);
    }
}
