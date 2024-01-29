using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // This is an "attribute" of the System library.
public struct SpawnerBehaviorControl
{
    [Tooltip("Score at which the conditions below are applied.")]
    public int scoreThreshold;
    [Tooltip("X: Multiplier. Y: Multiplier Swap Probability. The sum of all probabilities must be equal to 1.")]
    public Vector2[] multiplierSwapProbability;
    public List<GameObject> obstaclesPool;
}
