using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : MonoBehaviour
{
    [SerializeField] List<GameObject> asteroids = new List<GameObject>();

    Queue<GameObject> availableAsteroids;

    public static AsteroidPool Instance { get; private set; }

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
        availableAsteroids = new Queue<GameObject>();

        CreatePool();
    }

    void CreatePool()
    {
        foreach (GameObject asteroid in asteroids)
        {
            GameObject newAsteroid = Instantiate(asteroid, transform);
            availableAsteroids.Enqueue(newAsteroid);
            newAsteroid.SetActive(false);
        }
    }

    public GameObject SpawnAsteroid(Vector3 pos)
    {
        if (availableAsteroids.Count == 0)
        {
            CreatePool();
        }

        GameObject asteroid = availableAsteroids.Dequeue();
        asteroid.SetActive(true);
        asteroid.transform.position = pos;

        return asteroid;
    }

    public void AddAsteroidToPool(GameObject asteroid)
    {
        availableAsteroids.Enqueue(asteroid);

        asteroid.SetActive(false);
    }
}
