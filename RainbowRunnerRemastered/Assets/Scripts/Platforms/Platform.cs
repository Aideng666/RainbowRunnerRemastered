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

    LayerMask color1Layer;
    LayerMask color2Layer;
    LayerMask color3Layer;

    MeshRenderer mesh;

    private void OnEnable()
    {
        mesh = GetComponent<MeshRenderer>();
        color1Layer = LayerMask.NameToLayer("Color1");
        color2Layer = LayerMask.NameToLayer("Color2");
        color3Layer = LayerMask.NameToLayer("Color3");

        //Makes the starting platform stay on default layer
        if (isStartingPlatform)
        {
            return;
        }

        //Chooses a random starting color for a platform to be when it spawns
        switch (Random.Range(0, 3))
        {
            case 0:

                mesh.material = color1Mat;
                gameObject.layer = color1Layer;

                break;

            case 1:

                mesh.material = color2Mat;
                gameObject.layer = color2Layer;

                break;

            case 2:

                mesh.material = color3Mat;
                gameObject.layer = color3Layer;

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the platforms towards the player
        if (GameManager.Instance.gameStarted)
        { 
            transform.position += platformMoveSpeed * Time.deltaTime * Vector3.back;
        }

        //Adds past platforms back into the object pool (ignores the starting platform)
        if (transform.position.z <= -20 && isStartingPlatform)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z <= -20)
        {
            PlatformSpawner.Instance.DeactivatePlatform(gameObject);
        }   
    }
}
