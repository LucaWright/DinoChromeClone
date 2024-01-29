using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollider : MonoBehaviour
{
    public bool OnContactGround;
    // Since this variable is public, the CharacterBehaviour script, which has a reference to this component script, can both read and modify its value.
    // We are specifically interested in reading it (getting the value without setting the value). How do you do that? This way:
    // public bool OnContactGround { get; private set; }

    // The GroundCheck collider interacts exclusively with colliders in the Ground Layer.
    // To customize collision layers, navigate to Edit > Project Settings... > Physics2D > Collision Matrix.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnContactGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnContactGround = false;
    }
}
