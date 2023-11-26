using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnerCooldownController
{
    [Tooltip("Score at which the conditions below are applied.")]
    public int scoreLimit;
    [Tooltip("Set the Min Cooldown Multiplier and Probability. The sum of probabilities must be equal to 1.")]
    public Vector2[] cooldownMultiplyer_Probability = new Vector2[]
    {
        new Vector2(1f,     .0f),
        new Vector2(1.25f,  .0f),
        new Vector2(1.5f,   .25f),
        new Vector2(1.75f,  .5f),
        new Vector2(2f,     .25f),
    };

}
