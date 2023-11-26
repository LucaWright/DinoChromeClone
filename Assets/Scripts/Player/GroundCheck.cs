using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool OnContactGround;

    // GroundCheck collides with colliders in Ground Layer only!
    // Check Edit > Project Settings... > Physics2D > Collision Matrix
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnContactGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnContactGround = false;
    }
}
