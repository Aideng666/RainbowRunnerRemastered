using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_DoubleScore : Powerup
{
    PlayerScore score;

    protected override void Start()
    {
        base.Start();

        score = FindObjectOfType<PlayerScore>();
    }

    protected override void ApplyPowerup()
    {
        base.ApplyPowerup();

        score.ScoreMultiplier *= 2;
    }

    protected override void RemovePowerup()
    {
        score.ScoreMultiplier /= 2;

        base.RemovePowerup();
    }
}
