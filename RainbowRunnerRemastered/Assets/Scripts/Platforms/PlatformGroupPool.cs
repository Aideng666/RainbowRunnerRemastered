using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGroupPool : MonoBehaviour
{
    [SerializeField] PlatformSpawner spawner;
    GameObject[] platformGroups;

    List<PlatformGroup> availableGroups;

    public static PlatformGroupPool Instance { get; private set; }

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
        availableGroups = new List<PlatformGroup>();
    }

    GameObject SpawnNewGroup(int groupNum, Vector3 pos)
    {
        platformGroups = spawner.platformGroups;

        foreach (GameObject group in platformGroups)
        {

            if (group.GetComponent<PlatformGroup>().GroupNum == groupNum)
            {
                GameObject spawnedGroup = Instantiate(group, transform);

                spawnedGroup.transform.position = pos;

                return spawnedGroup;
            }
        }

        return null;
    }

    public GameObject SpawnGroup(int groupNum, Vector3 pos)
    {
        if (availableGroups.Count == 0)
        {

            return SpawnNewGroup(groupNum, pos);
        }
        else
        {
            foreach (PlatformGroup group in availableGroups)
            {
                if (group.GroupNum == groupNum)
                {
                    PlatformGroup spawnedGroup = group;

                    availableGroups.Remove(group);

                    spawnedGroup.transform.position = pos;
                    spawnedGroup.gameObject.SetActive(true);

                    return spawnedGroup.gameObject;
                }
            }
        }

        return SpawnNewGroup(groupNum, pos);
    }

    public void AddGroupToPool(GameObject group)
    {
        availableGroups.Add(group.GetComponent<PlatformGroup>());

        group.SetActive(false);
    }
}
