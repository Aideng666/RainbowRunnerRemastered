using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject connectedPortal;
    [SerializeField] Transform player;
    [SerializeField] bool isEntrance;

    private void OnCollisionEnter(Collision collision)
    {
        if (isEntrance && collision.gameObject.CompareTag("Player"))
        {
            player.position = connectedPortal.transform.position;
        }
    }
}
