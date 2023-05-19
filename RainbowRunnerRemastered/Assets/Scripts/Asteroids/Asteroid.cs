using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : MonoBehaviour
{
    Vector3 randomRotationVector;

    private void OnEnable()
    {
        randomRotationVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            transform.position += Vector3.back * GameManager.Instance.AsteroidMoveSpeed * Time.deltaTime;
            transform.Rotate(randomRotationVector * 50 * Time.deltaTime);
        }

        //Adds completed groups back into the object pool
        if (transform.position.z <= -20)
        {
            AsteroidPool.Instance.AddAsteroidToPool(gameObject);
        }
    }
}
