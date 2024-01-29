using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool jumpPressed;
    public bool jumpHoldDown;
    public bool jumpReleased;
    // A boolean variable returns either 'true' or 'false'.
    // When you declare a bool, its default value is 'false'.

    void Update()
    {
        jumpPressed = Input.GetButton("Jump");          // True of False?
        jumpHoldDown = Input.GetButtonDown("Jump");     // True of False?
        jumpReleased = Input.GetButtonUp("Jump");       // True of False?
        // Inputs here above are managed by the (old) input manager.
        // You can check what keyboard key or gamepad button is bound to "Jump" in Edit > Project Settings > Input Manager > Axes
    }
}
