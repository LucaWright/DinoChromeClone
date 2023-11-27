using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterBodyCollider : MonoBehaviour
{
    public UnityEvent OnGameOverEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            OnGameOverEvent.Invoke();
        }
    }
}
