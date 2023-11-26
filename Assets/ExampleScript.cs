// HI!
// This is a COMMENT!
// 🢇Press the [+] button to expand a question
#region WTF IS A COMMENT?
// A comment is a line not read and interpreted by the code.
// Its primary purpose is to write notes that aid in understanding the functions of the code itself.
#endregion 
#region HOW CAN I WRITE A COMMENT?
// A comment always starts with a double slash //.

/* If you wish to SPREAD THE COMMENT over multiple lines:
 * Begin the comment with /*;
 * Press Enter, and a new line of comment starting with * will appear;
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
/* That's because the only library used in the class below is UnityEngine.
 * Within the UnityEngine library, you'll find the MonoBehaviour class,
 * which allows us to call the Start and Update methods.
 * 
 * TRY THIS 
 * IF YOU ATTEMPT to comment out "using UnityEngine;" with //,
 * you'll notice an error related to MonoBehaviour.
 * Errors in the "INITIALIZATION =>DECLARATION OF VARIABLES\REFERENCES TO OTHER COMPONENTS" section can be observed as well.
 * Neither Vector2 nor RigidBody2D are recognized by the code
 * since they are part of the MonoBehaviour library.
 */
#endregion
#region CURIOSITY: HOW DO I SWITCH ON ANOTHER LIBRARY?
/* In the "INITIALIZATION =>DECLARATION OF VARIABLES" section,
 * attempt to declare a variable of type ArrayList.
 * Write: ArrayList myArrayList = new ArrayList();
 * ArrayList is a structure from the System.Collection library.
 * Now, 'using System.Collection;' should be switched on.
 */
#endregion

//🡻Class. Class name, MonoBehaviour, etc..
public class ExampleScript : MonoBehaviour
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
    public /*.1*/
    float /*.2*/
    speed /*.3*/
    = 1f /*.4*/
    ; /*.5*/
    //
    /*  .1 Public:
            If marked as Public, the variable will be EXPOSED IN THE SCRIPT COMPONENT INSPECTOR (observe the editor).
        .2 Variable Type:
            Is this a number (int or float)? Is it a bool or a string?
        .3 Variable Key Name:
            What do we call the value stored here? Choose an appropriate name.
        .4 (OPTIONAL) Variable Default Value:
            You can set a default value upon declaration. This value can be changed in the inspector.
            A floating-point number (float) needs the 'f' at the end.
        .5 Single Instruction End:
            An instruction, such as declaring a variable, can span multiple lines. The code considers the instruction complete with the ';' symbol.
    */

    public Vector2 gravity = new Vector2();
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
        //Comment the line above to set the 'speed' variabile through the inspector.
        
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
    void Update() //🡸Update is called once per frame
    {

    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        
    }
    #endregion

    #region DECLARATION OF METHODS
    // WHAT IS IT?
    // HOW TO CREATE
    // HOW TO CALL
    #endregion
}
