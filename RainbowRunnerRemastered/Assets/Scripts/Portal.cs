using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject connectedPortal;
    [SerializeField] bool isEntrance;

    PlayerMovement player;
    MeshRenderer mesh;

    private void Start()
    {
        player = PlayerMovement.Instance;
        mesh = GetComponentInChildren<MeshRenderer>();
    }

    //private void OnEnable()
    //{
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (isEntrance && other.CompareTag("Player"))
        {
            player.transform.position = connectedPortal.transform.position;
        }
        //else if (!isEntrance && other.CompareTag("Player"))
        //{
        //    mesh.enabled = false;
        //}
    }
}
