using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGroundBehavior : MonoBehaviour
{
    public float speed = 10f;
    // This value will be set by the GameManager.

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

}
