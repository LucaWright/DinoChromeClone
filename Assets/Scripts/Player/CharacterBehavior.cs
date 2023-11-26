using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    [Header("Token Properties")] // This is an "attribute". What the Header attribute do? Check it out in the editor inspector!
    [Tooltip("Absolute value of Gravity Acceleration")] // This is an "attribute". What the tooltip attribute do? Check it out moving the mouse over "Gravity" variable in the editor inspector!
    public float gravity = 10f;
    [Tooltip("Height of a jump, in meters")] // This is an "attribute". What the tooltip attribute do? Check it out moving the mouse over "Jump Height" variable in the editor inspector!
    public float jumpHeight = 3f;

    [Space] // This is an "attribute". What the spase attribute do? Check it out in the editor inspector!
    [Header("Token References")] // This is an "attribute". What the Header attribute do? Check it out in the editor inspector!
    public Rigidbody2D rb; //Reference of RigidBody.
    public CharacterController controller;
    public GroundCheck groundCheck;
    public CharacterBodyCollider bodyCollider;
    public BoxCollider2D scoreTriggerBox;


    void Start()
    {
        // Set RigidBody2D's "gravity scale".
        // It's our output gravity value divided 9.81 (the editor physics default gravity you can check in Edit ► Project Settings... ► Physics 2D ► Gravity)
        // Physics2D is the Library. Gravity is a Vector2. Pick the y value (-9.81) and return the absolute value
        rb.gravityScale = gravity / Mathf.Abs(Physics2D.gravity.y);

        rb.freezeRotation = true;

        //scoreTriggerBox setup
        scoreTriggerBox.offset += Vector2.down * (jumpHeight / 2f);
        scoreTriggerBox.size = new Vector2(0.25f, jumpHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.keyPressed && groundCheck.OnContactGround)
        {
            //Call the method Jump()
            Jump();
        }

        //CHALLENGE!
        #region InputChallenge
        if (controller.keyHoldDown)
        {

        }

        if (controller.keyReleased)
        {

        }
        #endregion
    }

    void Jump()
    {
        // Change momentum
        var momentum = Mathf.Sqrt(2f * gravity * jumpHeight);
        rb.velocity = momentum * Vector2.up;
        // ChangeAnimation?
        // PlaySound?
    }
}
