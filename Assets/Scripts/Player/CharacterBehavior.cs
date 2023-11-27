using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    [Header("Token Properties")] // This is an "attribute". What the Header attribute do? Check it out in the editor inspector!
    [Tooltip("Absolute value of Gravity Acceleration")] // This is an "attribute". What the tooltip attribute do? Check it out moving the mouse over "Gravity" variable in the editor inspector!
    public float gravity = 50f;
    [Tooltip("Height of a jump, in meters")] // This is an "attribute". What the tooltip attribute do? Check it out moving the mouse over "Jump Height" variable in the editor inspector!
    public float jumpHeight = 3f;

    [Space] // This is an "attribute". Check its effect in the inspector!
    [Header("Token References")] // This is an "attribute". Check its effect in the inspector!
    public Rigidbody2D rb; // Reference to the Rigidbody2D.
    [Tooltip("Drag & drop the Controller GameObject onto this field in the inspector.")]
    public CharacterController controller; // Reference to the CharacterController component script.
    [Tooltip("Drag & drop the GroundCheckCollider GameObject onto this field in the inspector.")]
    public GroundCheck groundCheck; // Reference to the GroundCheck component script. GroundCheck collides only with the Ground Layer.
    [Tooltip("Drag & drop the BodyCollider GameObject onto this field in the inspector.")]
    public CharacterBodyCollider bodyCollider; // Reference to the CharacterBodyCollider component script. This collider detects obstacles, triggering the Game Over Event by the CharacterBodyCollider script.
    [Tooltip("Drag & drop the ScoreTrigger GameObject onto this field in the inspector.")]
    public BoxCollider2D scoreTriggerBox; // Reference to the BoxCollider2D component. This is a trigger that detects jumped obstacles and adds score.


    void Start()
    {
        rb.gravityScale = gravity / Mathf.Abs(Physics2D.gravity.y);
        // Sets the RigidBody2D's "gravity scale" based on the output gravity value (50) divided by 9.81 (the default gravity in the editor, which you can find in Edit ► Project Settings... ► Physics 2D ► Gravity).
        // Physics2D is the library, and Gravity is a Vector2(0, -9.81f). We take the y value (-9.81) and use its absolute value (9,81).
        // Now, Physics2D.Gravity multiplied by Gravity Scale equals the output gravity of this script.

        rb.freezeRotation = true;
        // Sets the FreezeRotation checkbox in the RigidBody2D using code.

        scoreTriggerBox.isTrigger = true;
        scoreTriggerBox.offset += Vector2.down * (jumpHeight / 2f);
        scoreTriggerBox.size = new Vector2(0.25f, jumpHeight); // WTF is that "new"? Check the answer in ExampleScript. Ask to Google. Ask to an AI. Find your answers by yourself!
        // Sets up the scoreTriggerBox collider.
        // Try to understand how this collider shape is modified and why.
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.keyPressed && groundCheck.OnContactGround) // Condition
        {
            Jump();
            //Call the method Jump()
        }

        // CHALLENGE!
        #region InputChallenge
        // Considering adjusting some inputs?
        // Perhaps you'd like to create a function to control jump intensity based on the duration of the key being held down.
        // Maybe you're interested in adding a character action, like a dash or a smackdown move while in the air.
        // Feel free to explore and design something unique!
        if (controller.keyHoldDown)
        {

        }

        if (controller.keyReleased)
        {

        }
        #endregion
    }

    void Jump() // Jump Method Instruction List
    {
        // Adjusting momentum. First, calculate the momentum...
        var momentum = Mathf.Sqrt(2f * gravity * jumpHeight);
        // ...then, apply the calculated momentum to the Rigidbody2D's velocity.
        rb.velocity = momentum * Vector2.up;

        // ChangeAnimation?
        // PlaySound?
    }
}
