using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGroup : MonoBehaviour
{
    [SerializeField] int groupNum;
    [SerializeField] float length;

    public int GroupNum { get { return groupNum; }}
    public float GroupLength { get { return length; }}

    private void Update()
    {
        //Moves the platforms towards the player
        if (GameManager.Instance.gameStarted)
        {
            transform.position += GameManager.Instance.PlatformMoveSpeed * Time.deltaTime * Vector3.back;
        }

        //Adds completed groups back into the object pool
        if (transform.position.z + length <= -30)
        {
            PlatformGroupPool.Instance.AddGroupToPool(gameObject);
        }
    }
}
