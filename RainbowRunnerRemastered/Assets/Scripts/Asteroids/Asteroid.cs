using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : MonoBehaviour
{
    private void OnEnable()
    {
        //Animates the asteroid being spawned in
        transform.localScale += Vector3.zero;
        transform.DOScale(Vector3.one * 200, 2).SetEase(Ease.OutBack);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            transform.position += Vector3.back * GameManager.Instance.AsteroidMoveSpeed * Time.deltaTime;
        }

        //Adds completed groups back into the object pool
        if (transform.position.z <= -20)
        {
            AsteroidPool.Instance.AddAsteroidToPool(gameObject);
        }
    }
}
