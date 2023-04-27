using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Material color1Mat;
    [SerializeField] Material color2Mat;
    [SerializeField] Material color3Mat;
    [SerializeField] float platformMoveSpeed = 10;
    [SerializeField] bool isStartingPlatform = false;

    MeshRenderer mesh;

    private void OnEnable()
    {
        mesh = GetComponent<MeshRenderer>();

        switch (Random.Range(0, 3))
        {
            case 0:

                mesh.material = color1Mat;

                break;

            case 1:

                mesh.material = color2Mat;

                break;

            case 2:

                mesh.material = color3Mat;

                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
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

    public void SetStartingPlatform()
    {
        isStartingPlatform = true;
    }
}
