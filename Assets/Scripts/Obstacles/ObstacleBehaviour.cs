using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    // This script manages the destruction of obstacles when outside the camera view
    private float deadZone;

    void Start()
    {
        // Pick the main camera's transform
        var camTransform = Camera.main.transform;
        // Sets the dead zone on the opposite side of the screen, based on the spawner's horizontal position relative to the camera.
        deadZone = camTransform.position.x - (transform.position.x - camTransform.position.x);
    }

    private void FixedUpdate()
    {
        // Check if the obstacle is outside the camera view
        if (transform.position.x <= deadZone)
        {
            // Destroy the GameObject.
            // NOTE: "this" refers to this script component. Destroying "this" destroys the script component, not the GameObject!
            Destroy(this.gameObject);
        }
    }
}
