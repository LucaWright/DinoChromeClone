using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    // This script manages the destruction of obstacles when outside the camera view
    private float deadZone;
    private float speed;
    private SpawnerBehaviour spawnerBehavior;

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime; // Move the obstacle.
        
        if (transform.position.x <= deadZone) // Check if the obstacle is outside the camera view.
        {
            spawnerBehavior.DestroyObstacle(this);
        }
    }

    public void SetDeadZone(float value)
    {
        deadZone = value;
    }

    public void SetNewSpeed(float value)
    {
        speed = value;
    }

    public void ObstacleSetup(float newDeadzone, float newSpeed, SpawnerBehaviour spawnerReference)
    {
        deadZone = newDeadzone;
        speed = newSpeed;
        spawnerBehavior = spawnerReference;
    }
}
