using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleCollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) // If the collider attached to this GameObject hits something...
    {
        if (collision.gameObject.CompareTag("Obstacle")) // ...and that something is a GameObject tagged as "Obstacle" (you can create and assign a tag for each GameObject in the editor)...
        {
            GameManager.instance.GameOver(); // ...then, notify the Game Manager to call the GameOver() method.
        }
    }
}
