using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_SpaceWalker : Powerup
{
    PlayerMovement player;

    protected override void Start()
    {
        base.Start();

        player = FindObjectOfType<PlayerMovement>();
    }

    protected override void ApplyPowerup()
    {
        base.ApplyPowerup();

        player.MoveSpeed *= 1.5f;
    }

    protected override void RemovePowerup()
    {
        player.MoveSpeed /= 1.5f;

        base.RemovePowerup();
    }
}
