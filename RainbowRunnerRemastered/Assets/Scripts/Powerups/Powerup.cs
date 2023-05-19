using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] Powerups powerupType;
    [SerializeField] Material color1Mat;
    [SerializeField] Material color2Mat;
    [SerializeField] Material color3Mat;

    LayerMask color1Layer;
    LayerMask color2Layer;
    LayerMask color3Layer;

    MeshRenderer mesh;

    protected float elaspedPowerupTime = 0;
    protected bool powerupActivated = false;

    public Powerups PowerupType { get { return powerupType; } }

    private void OnEnable()
    {
        mesh = GetComponent<MeshRenderer>();

        color1Layer = LayerMask.NameToLayer("Color1");
        color2Layer = LayerMask.NameToLayer("Color2");
        color3Layer = LayerMask.NameToLayer("Color3");

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

    // Start is called before the first frame update
    protected virtual void Start()
    {
        elaspedPowerupTime = 0;
        powerupActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            transform.position += Vector3.back * GameManager.Instance.PlatformMoveSpeed * Time.deltaTime;
        }

        if (powerupActivated)
        {
            if (elaspedPowerupTime >= GameManager.Instance.PowerupDuration)
            {
                RemovePowerup();
            }

            elaspedPowerupTime += Time.deltaTime;
        }
    }

    protected virtual void ApplyPowerup()
    {
        powerupActivated = true;
    }

    protected virtual void RemovePowerup()
    {
        powerupActivated = false;

        PowerupPool.Instance.AddPowerupToPool(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerup();
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

public enum Powerups
{
    DoublePoints,
    SpaceWalker
}

