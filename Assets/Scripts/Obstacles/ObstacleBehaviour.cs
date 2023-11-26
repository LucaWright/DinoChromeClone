using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private float deadZone;

    void Start()
    {
        var camTransform = Camera.main.transform;
        deadZone = camTransform.position.x - (transform.position.x - camTransform.position.x);
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= deadZone)
        {
            Destroy(this.gameObject);
        }
    }
}
