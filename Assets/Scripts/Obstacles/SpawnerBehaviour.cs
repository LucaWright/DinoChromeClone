using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerBehaviour : MonoBehaviour
{
    public GameObject movingGround;
    public List<GameObject> obstacleList = new List<GameObject>();
    public float baseCooldown = .5f;
    public float cooldown = .5f;
    public float allowance = .1f;
    private float timer;

    public UnityEvent SetNewCooldownValue;

    void Start()
    {
        // Setting baseCooldown: 2f * (PlayerJumpMomentum / GravityAcceleration) + allowance

        // Get the Player GameObject, then retrieve the CharacterBehavior component
        var playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>();
        // Calculate baseCooldown
        baseCooldown = 2f * Mathf.Sqrt(2f * playerBehavior.gravity * playerBehavior.jumpHeight) / playerBehavior.gravity + allowance;

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cooldown)
        {
            // Instantiate a random obstacle GameObject from the obstacleList.
            Instantiate(
                obstacleList[Random.Range(0, obstacleList.Count)],
                transform.position, transform.rotation, // Set spawn position and rotation (based on the spawner's position and rotation).
                movingGround.transform // Parent the instantiated obstacle to the movingGround GameObject's transform.
            );

            // Reset the timer
            timer -= cooldown;

            // Invoke the SetNewCooldownValue event
            SetNewCooldownValue.Invoke();
        }
    }
}
