using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] float zSpawnPos = 20;

    public GameObject[] platformGroups { get; private set; }

    float distanceBetweenPlatforms = 15;
    bool platformsSpawned = false;

    GameObject currentGroup = null;

    public GameObject CurrentGroup { get { return currentGroup; } }
     
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

        platformGroups = GetAllPlatformGroups();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameStarted && !platformsSpawned && PlatformGroupPool.Instance != null)
        {
            int groupChoice = Random.Range(1, platformGroups.Length + 1);

            GameObject group = PlatformGroupPool.Instance.SpawnGroup(groupChoice, new Vector3(0, 0, 10));

            currentGroup = group;

            if (currentGroup != null)
            {
                platformsSpawned = true;
            }
        }

        if (GameManager.Instance.gameStarted)
        {
            if (currentGroup.transform.position.z + currentGroup.GetComponent<PlatformGroup>().GroupLength + distanceBetweenPlatforms < zSpawnPos)
            {
                int groupChoice = Random.Range(1, platformGroups.Length + 1);

                GameObject group = PlatformGroupPool.Instance.SpawnGroup(groupChoice, new Vector3(0, 0, zSpawnPos));

                currentGroup = group;
            }
        }
    }

    public GameObject[] GetAllPlatformGroups()
    {
        return Resources.LoadAll<GameObject>("PlatformGroups");
    }
}
