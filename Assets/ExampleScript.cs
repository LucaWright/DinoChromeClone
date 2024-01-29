// HI!
// This is a COMMENT!
// 🢇Press the [+] button to expand a question
#region WTF IS A COMMENT?
// A comment is a line not read and interpreted by the code.
// Its primary purpose is to write notes that aid in understanding the functions of the code itself for those who reading.
#endregion 
#region HOW CAN I WRITE A COMMENT?
// In C#, comments can be inserted in two ways:

// 1. Single-line comments: start with two slashes (//)
// and everything that follows on the same line is considered a comment.

/* 2. Multi-line comments: start with a slash and an asterisk (/*) and press Enter:
 * a new line of comment starting with * will appear.
 * To conclude the comment, type: 
 */

// You can /* open and close */ comments in the middle of a line of code too!
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region 🡹WTF ARE THESE THINGS ABOVE PRECEDED BY THE "using" KEYWORD?
// They are the LIBRARIES of ready-to-use methods made available by the Unity Engine.
#endregion
#region "using UnityEngine;" IS SWITCHED ON. WHY?
/* That's because the only library used in the class below (called ExampleScript) is UnityEngine.
 * Within the UnityEngine library, you'll find the MonoBehaviour class,
 * which allows us to call the Start and Update methods.
 * 
 * TRY THIS 
 * Comment out "using UnityEngine;" with "//".
 * You'll notice an error related to MonoBehaviour.
 * Errors in the "INITIALIZATION =>DECLARATION OF VARIABLES\REFERENCES TO OTHER COMPONENTS" section can be observed as well.
 * Neither Vector2 nor RigidBody2D are recognized by the code
 * since they are part of the MonoBehaviour library.
 */
#endregion
#region CURIOSITY: HOW DO I SWITCH ON ANOTHER LIBRARY?
/* In the "INITIALIZATION =>DECLARATION OF VARIABLES" section,
 * attempt to declare a variable of type ArrayList.
 * Write:
 * ArrayList myArrayList = new ArrayList();
 * ArrayList is a structure from the System.Collection library.
 * Now, 'using System.Collection;' should be switched on!
 */
#endregion

//🡻Class. The name of the class and the name of the script must be the same!
public class ExampleScript : MonoBehaviour // Your class inherits from MonoBehaviour.
#region ALL YOU NEED TO KNOW ABOUT MONOBEHAVIOUR FOR NOW
/* MonoBehaviour is a base class that all Unity scripts inherit from.
 * It provides many fundamental functions and features for game creation in Unity,
 * such as access to game lifecycle messages (e.g., Start(), Update(), etc.).
 * However, for beginners, you don’t need to worry too much about the details of MonoBehaviour. 
 * It’s enough to know that it’s an essential part of Unity that makes many of its features possible.
 */
#endregion
{
    #region 🢄WTF IS THIS CURLY BRACE? 
    /* All MonoBehaviour instructions must be contained within these curly brackets.
     * The open curly bracket denotes the beginning of the INSTRUCTION BLOCK.
     * Following the dashed line, you'll encounter the closed curly bracket
     * indicating the conclusion of the MonoBehaviour instruction block.
     */
    #endregion

    /* MonoBehaviour lifecycle goes through three phases:
     *      - Initialization
     *      - Update
     *      - Rendering
     * You can check all the methods and their execution order here below:
     * https://docs.unity3d.com/Manual/ExecutionOrder.html
     */
    #region INITIALIZATION
    /* The initialization phase in a MonoBehaviour
     * is the moment when initial operations are performed to prepare the object before the game update begins.
     * This phase include:
     */
    #region =>DECLARATION OF VARIABLES
    // What are the attributes of this behavior script?
    /*.1*/      public
    /*.2*/      float
    /*.3*/       speed
    /*.4*/      = 1f
    /*.5*/      ;
    //
    /*  .1 Public:
            If marked as Public, the variable will be EXPOSED IN THE SCRIPT COMPONENT INSPECTOR (observe the editor).
        .2 Variable Type:
            Is this a number (int or float)? Is it a bool or a string?
        .3 Variable Key Name:
            What do we call the value stored here? Choose an appropriate name.
        .4 (OPTIONAL) Variable Default Value:
            You can set a default value upon declaration. This value can be changed in the inspector.
            A floating-point number (float) needs the 'f' at the end, to distinguish them from doubles and decimals.
        .5 Single Instruction End:
            An instruction, such as declaring a variable, can span multiple lines. The code considers the instruction complete with the ';' symbol.
    */

    public Vector2 gravity = new Vector2(0, -10f);
    /* Vector2 is a structure (struct) found in the UnityEngine library.
     * This struct is comprised of 2 floats (x,y), and thus needs to be instantiated.
     * To initialize it, you must create an instance using the 'new' keyword,
     * and call a method specifying the structure type (Vector2).
     * Then follow the constructor's instructions.
     */
    #endregion
    #region =>REFERENCES TO OTHER COMPONENTS
    public Rigidbody2D rb;
    /* This is a RigidBody2D reference.
     * Remember to assign the reference with the appropriate component, or you will encounter the NullReference error!
     * The reference can be any component, even one on another game object!
     * It can also be another custom Monobehaviour of yours
     * (like the CharacterController inside the CharacterBehavior in the game project)
     */
    #region ==>HOW THE DO I ASSIGN THE REFERENCE?
    /* There are different ways to assign a reference:
     *      .1 If the reference is public,
     *          it's enough to drag & drop the component
     *          into the corresponding exposed field of this script in the Unity editor
     *          (in this case: rb).
     *      .2 In one of the initialization methods of MonoBehaviour (I.E.: Start).
     *          If the component is in the same GameObject,
     *          just call the GetComponent<TypeOfComponent> method.
     *          In our case:
     *          rb = GetComponent<Rigidbody2D>();
     *      .3 Other ways through code.
     * Again: remember to assign the reference with the appropriate component,
     * or you will encounter the NullReference error!
     */
    #endregion
    #endregion
    #region =>INITIAL CONFIGURATION
    // You can SET VALUES and GETTING COMPONENTS in the Start method below
    void Start() //🡸Start is called before the first frame update
    {
        #region 🢄A CURLY BRACE? AGAIN? 
        /* All Start method instructions must be contained within these curly brackets.
         * The open curly bracket denotes the beginning of the INSTRUCTION BLOCK.
         * Following the dashed line, you'll encounter the closed curly bracket
         * indicating the conclusion of the Start method instruction block.
         */
        #endregion
        #region ==>SET VALUES
        speed = 2f; // Accesses the variable declared with the 'speed' key name and sets its float value to 2.
        //
        /* You can set an initial value for a variable here.
         * This will be initialized, overwriting the value in the inspector!
         * Here is the set priority:
         *      1. Value set in the Start method
         *      2. Value set in the Unity Inspector (outside execution)
         *      3. Default value assigned at declaration
         */
        //
        // Comment the code line above to set the 'speed' variabile through the inspector.
        
        gravity = new Vector2(0, -50f);
        /* Vector2 is a structure (struct) found in the UnityEngine library.
         * This struct is comprised of 2 floats (x,y), and thus needs to be instantiated.
         * To initialize it, you must create an instance using the 'new' keyword,
         * and call a method specifying the structure type (Vector2).
         * Then follow the constructor's instructions.
         */
        #endregion
        #region ==>GET COMPONENTS
        rb = GetComponent<Rigidbody2D>(); // Get the RigidBody2D from this GameObject, then assign it to the 'rb' reference.
        /* There are different ways to assign a reference:
         *      .1 If the reference is public,
         *          it's enough to drag & drop the component
         *          into the corresponding exposed field of this script in the Unity editor
         *          (in this case: rb).
         *      .2 As above.
         *          If the component is in the same GameObject,
         *          just call the GetComponent<TypeOfComponent> method.
         *          This method checks all components in the Game Object (our Token)
         *          and it stops when it finds the component of the type specified inside these angle brackets (< and >).
         *          In our case:
         *          rb = GetComponent<Rigidbody2D>();
         *      .3 Other ways through code.
         */

        #endregion
    }
    #region 🢄AND THIS CURLY BRACE? 
    // This marks the conclusion of the Start method instruction block.
    #endregion
    #endregion
    #region =>BONUS: OTHER INITIALIZATION METHODS
    private void Awake() { }
    private void OnEnable() { }
    // These methods are called before Start.
    // You can check their execution order here: https://docs.unity3d.com/Manual/ExecutionOrder.html
    #endregion
    #endregion

    #region UPDATE
    /* After initialization, the Update loop starts.
     * Update is called once per frame. This means it runs many times per second (depending on the game's framerate).
     * This is where you would put the game's logic, the code for the game's instructions.
     * The Update loop is crucial for the game dynamics, as it allows to continuously monitor the game conditions
     * and respond in real time to player inputs.
     */

    void Update() //🡸Update is called once per frame
    {
        #region 🢄CURLY BRACES. DID YOU GET IT? 
        /* All Update method instructions must be contained within these curly brackets.
         * The open curly bracket denotes the beginning of the INSTRUCTION BLOCK.
         * Following the dashed line, you'll encounter the closed curly bracket
         * indicating the conclusion of the Start method instruction block.
         */
        #endregion
        #region =>WHAT DO I HAVE TO WRITE HERE?
        /* In the Update() method, we put the game logic,
         * which is the code that determines how the game responds to player inputs and how it changes over time.
         * This can include things like character movement, collision handling, score updating, and so on.
         */
        // Here’s an example of what game logic might look like inside the Update() method:
        
        if (Input.GetKeyDown("Space"))
        {
            Jump();
        }
        #region ==>🢄SLOW DOWN! WHAT'S THAT?
        /* An if statement is a programming construct that execute the instruction block in the curly braces {}
         * when the boolean condition inside the parenthesis () is true.
         * In this example: if the space key bar is pressed, then call che Jump() method.
         */
        #region ===>WTF IS "Jump()"?
        // Calls the Jump() method.
        // Check the "DECLARATION OF METHODS" section.
        #endregion
        #endregion
        #endregion
    }
    #region =>BONUS: OTHER UPDATE METHODS
    private void FixedUpdate() //🡸FixedUpdate is called at regular time intervals, regardless of the framerate.
    {
        // It’s the best place to put code that needs to run consistently over time, like game physics.
    }

    private void LateUpdate() //🡸LateUpdate is called after all Update() methods.
    {
        /* For example, a follow camera should always be implemented in LateUpdate()
         * because it tracks objects that might have moved inside Update().
         * In other words, LateUpdate() is commonly used for operations that depend on what happened during Update().
         */
    }
    #endregion
    #endregion

    #region METHODS
    #region =>DECLARATION OF METHODS
    // What are the methods of this behavior script?
    /*.1*/
    public
    /*.2*/      void
    /*.3*/      Jump
    /*.4*/      ()
    /*.5*/      {
                    // Change physics;
                    // Change animation;
                    // Play sound;
                    // ...
                }
    //
    /*  .1 Public:
            If marked as Public, this method can be invoked from other classes.
        .2 Return Type:
            This is the type of value that the method returns.
            In this case, "void" means the method does not return a value. It will become clearer with the next example.
        .3 Method Key Name:
            This is the name of the method. It should be descriptive of what the method does.
        .4 Parameters:
            These are the inputs to the method.
            This method does not take any parameters, as indicated by the empty parentheses ().  It will become clearer with the next example.
        .5 Instruction block
            This is where the instructions that the method will execute are written.
            These instructions are executed when the method is called.
    */

    // Take this method:
    public int Sum(int a, int b)
    {
        int result = a + b;
        return result;
    }
    /*  This time, this method has a return type ("int" instead of "void")
     *  and some input parameters (an integer called "a" and an integer called "b").
     *  This method adds the input integers, "a" and "b".
     *  It creates the "return" temporary variable (called a local variable, because it exists only inside the method), result,
     *  then returns the value.
     *  If you write in the Start() or Update() method
     *      Debug.Log(Sum(2,3));
     *  the console will return the value of 5;
     */
    #endregion
    #region =>CALL A METHOD
    /* You can call a method directly using its name and passing the necessary parameters inside the parentheses ().
     * If the method does not take any parameters, use empty parentheses ().
     */
    #endregion
    #endregion

}
// Go to CharacterBehavior script
