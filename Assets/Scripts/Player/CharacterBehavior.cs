using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    #region DECLARATION OF VARIABLES
    [Header("Token Properties")] // This is another 'attribute'. Observe its effect in the Inspector! (Try commenting out this line with // to see the differences.)
    [Tooltip("Absolute value of Gravity Acceleration")] // This is an 'attribute'. What the tooltip attribute do? Check it out moving the mouse over "Gravity" variable in the editor inspector!
    public float gravity = 50f;
    [Tooltip("Height of a jump, in meters")] // This is an 'attribute'. What the tooltip attribute do? Check it out moving the mouse over 'Jump Height' variable in the editor inspector!
    public float jumpHeight = 3f;

    [Space] // This is another 'attribute'. Check its effect in the inspector! (Try commenting this line with // to watch the differences.)
    [Header("Token References")] // This is another 'attribute'. It's useful for keeping your custom component (this script) organized.
    public Rigidbody2D rb; // Reference to the Rigidbody2D.
    [Tooltip("Drag & drop the Controller GameObject onto this field in the inspector.")]
    public CharacterController controller; // Reference to the CharacterController component script.
    [Tooltip("Drag & drop the GroundCheckCollider GameObject onto this field in the inspector.")]
    public TerrainCollider terrainCollider; // Reference to the TerrainCollider component script. GroundCheck collides only with the Ground Layer.
    [Tooltip("Drag & drop the ScoreTrigger GameObject onto this field in the inspector.")]
    public BoxCollider2D scoreTriggerBox; // Reference to the BoxCollider2D component. This trigger detects jumped obstacles and warns the Game Manager to add score.
    #region =>🡹COMPONENT-BASE ARCHITECTURE
    /*
     * This 'Player' GameObject (GO) follows a component-based architecture. 
     * Each child GameObject is responsible for a specific function, enhancing modularity and reusability.
     * 
     * - 'TerrainCollider': Manages the player's collision with the ground.
     * - 'ObstacleCollisionDetector': Manages the player's detection of collisions with obstacles.
     * - 'CharacterController': Contains the player's control logic.
     * - 'Renderer' handles the rendering of the player (sprites and animations).
     *    This component is currently empty, providing an opportunity for you to add your own animations.
     *    If you wish to enhance the player with animations, this is the subtoken with the 'render' component where you should add them.
     *
     * 'Player' (the parent GO) is equipped with a Rigidbody for physics interactions.
     * This design allows for a clear division of responsibilities among components, 
     * making the code more maintainable and scalable.
     * 
     * This makes it easier to explain all of the Token's functions.
     */
    #region ==>WHAT DOES EACH CUSTOM COMPONENT REFERENCE DO?
    /* Try this:
     * 1. Select 'TerrainCollider' (with capital T) by clicking on it;
     * 2. Press: F12.
     * You will jump to the 'TerrainCollider' script. Now you can check its function.
     * Do the same with 'CharacterController'.
     */
    #endregion
    #endregion
    #region =>PRO TIP: HOW TO CHANGE A VARIABLE NAME
    // Want to change once and for all the name of a varable?
    // Hold CTRL and then press in sequence R and R, then edit the name.
    #endregion
    #endregion

    #region INITIALIZATION
    void Start()
    {
        // Those methods are declarated in the INITIALIZATION METHODS section.
        RigidBody2DSetup();
        ScoreTriggerSetup();
    }
    #endregion

    #region INITIALIZATION METHODS
    void RigidBody2DSetup()
    {
        /* GRAVITY
         * There are two methods to adjust gravity.
         */

        /* 1. Alter the universal settings of Physics2D. By default,
         * Physics2D applies a gravity vector of (0, -9.81) to objects simulated with a rigidbody.
         * This can be modified by navigating to Edit > Project Settings > Physics2D > General Settings > Gravity.
         * However, it’s also possible to make this adjustment through code!
         */
        Physics2D.gravity = new Vector2(0, -gravity); // WTF is that 'new'? Check the answer in ExampleScript. Ask to Google. Ask to an AI. Find your answers by yourself!

        /* 
         * 2. Adjust the 'local gravity' affecting this specific Token by using the Gravity Scale.
         *    This is a multiplier for the base gravity vector set by Physics2D.
         *    Determining the correct Scale for our desired gravity involves some basic math.
         *    CURRENTLY, METHOD 1 IS BEING USED!
         *    THEREFORE, THE CODE FOR METHOD 2 IS COMMENTED OUT!
         */

        #region =>PRO TIP: HOW TO UNCOMMENT MULTIPLE LINES OF CODE
        // Select the part you want to uncomment.
        // Hold CTRL key. Then, press in sequence: K, U.
        // Remember to comment the line of code above (method 1)
        //
        #endregion
        //rb.gravityScale = gravity / Mathf.Abs(Physics2D.gravity.y);
        //// Sets the RigidBody2D's "gravity scale" based on the output gravity value (50) divided by 9.81 (the default gravity in the editor, which you can find in Edit ► Project Settings... ► Physics 2D ► Gravity).
        //// Physics2D is the library, and Gravity is a Vector2(0, -9.81f). We take the y value (-9.81) and use its absolute value (9,81).
        //// Now, Physics2D.Gravity multiplied by Gravity Scale equals the output "gravity" of this script.
        #region =>PRO TIP: HOW TO COMMENT AGAIN MULTIPLE LINES OF CODE
        // Holt CTRL key. Then, press in sequence: K, C.
        #endregion

        // ROTATION
        rb.freezeRotation = true;
        // Sets the FreezeRotation checkbox in the RigidBody2D using code.
        // It's the same of manually tick the checkbox in the RigidBody2D component inspector.
    }

    void ScoreTriggerSetup()
    {
        scoreTriggerBox.isTrigger = true;
        scoreTriggerBox.offset += Vector2.down * (jumpHeight / 2f);
        scoreTriggerBox.size = new Vector2(0.25f, jumpHeight);
        // Sets up the scoreTriggerBox collider.
        // Try to understand how this collider shape is modified and why. All the properties modified by the code are visible in the collider component inspector.
    }
    #endregion

    #region  UPDATE LOOP
    void Update()
    {
        if (controller.jumpPressed && terrainCollider.OnContactGround) // Conditions
        {
            Jump();
        }
        #region CONDITIONS: BOOLEAN LOGIC
        /* Boolean logic is a type of logic that deals with true and false values, called boolean values.
         * Here's an explanation of the boolean operations you mentioned:
         * .1   AND (&&)        : This operation returns 'true' if all of its operands are true. Otherwise, it returns 'false'.
         * .2   OR (||)         : This operation returns 'true' if at least one of its operands is true. Otherwise, it returns 'false'.
         * .3   Equal (==)      : This operation checks if two values are equal. If they are, it returns 'true'. Otherwise, it returns 'false'.
         * .4   Not Equal (!=)  : This operation checks if two values are different. If they are, it returns 'true'. Otherwise, it returns 'false'.
         * 
         * Use '!' before a bool (!bool) to return 'not bool'. If bool is true, !bool is false, and vice versa.
         * Do you need some inputs that only work when the character is flying? One of the conditions of the If statement should definitely be '!terrainCollider.OnContactGround'.
         */
        #endregion
        #region =>INPUT DESIGN CHALLENGE
        // Considering adjusting some inputs?
        // Perhaps you'd like to create a function to control jump intensity based on the duration of the key being held down.
        // Maybe you're interested in adding a character action, like a dash or a smackdown move while in the air.
        // Feel free to explore and design something unique!
        if (controller.jumpHoldDown)
        {

        }

        if (controller.jumpReleased)
        {

        }
        #endregion
    }
    #endregion

    #region UPDATE METHODS
    void Jump() // Jump Method Instruction List
    {
        // Adjusting momentum. First, calculate the momentum...
        var momentum = Mathf.Sqrt(2f * gravity * jumpHeight); // This is a kinematic formula used to calculate the initial velocity required for a jump to reach a certain height given a specific gravity.
        // ...then, apply the calculated momentum to the Rigidbody2D's velocity.
        rb.velocity = momentum * Vector2.up;

        /* Other operations for this method should be:
         * - Animation trigger (from run animation to jump animation)
         * - Jump sound effect trigger
         * - Jump particles trigger
         */
    }
    #endregion
}
