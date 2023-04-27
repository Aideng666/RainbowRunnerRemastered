using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float platformMoveSpeed = 10;
    [SerializeField] bool isStartingPlatform = false;

    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartingPlatform && !GameManager.Instance.gameStarted) 
        {
            return;
        }

        transform.position += platformMoveSpeed * Time.deltaTime * Vector3.back;

        if (transform.position.z <= -20 && !isStartingPlatform)
        {
            PlatformPool.Instance.AddPlatformToPool(gameObject);
        }   
    }
}
