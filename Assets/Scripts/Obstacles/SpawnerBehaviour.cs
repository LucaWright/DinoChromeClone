using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    /* === INTRODUCTION TO THE SPAWNER DESIGN ===
     * 
     * Before diving into the code, it's crucial to understand the design logic behind the Spawner.
     * The Spawner is a Token that plays a pivotal role in the game's system.
     * 
     * The Spawner's primary function is to manage the game's difficulty and pacing by spawning obstacles that the player must avoid.
     * However, its role extends beyond just spawning obstacles.
     *
     * To achieve a balanced gameplay experience, we need to ask ourselves some key questions:
     * 
     * 1. What is the minimum spawn rate?
     *    Below what rate does the game become impossible to win?
     *
     * 2. What is the maximum speed at which obstacles can move?
     *    Is there also a minimum speed?
     *
     * 3. Are there other 'dimensions' in which the game's difficulty can be developed?
     *    If so, how do they interact with the other dimensions?
     *
     * The answers to these questions will guide the design of the Spawner and determine how it contributes to the overall game design.
     */

    #region DECLARATION OF VARIABLES

    [Tooltip("The cooldown period between obstacle spawns. This determines the rate at which obstacles are generated.")]
    public float cooldown = .75f;
    [Tooltip("An allowance value used to prevent the game from requiring frame-perfect jumps, which would be humanly impossible to perform.")]
    public float baseCooldownAllowance = .2f;

    private float baseCooldown; // The base cooldown value, representing the minimum spawn rate. This is calculated in the SetBaseCooldown() method.
    private float timer; // A timer used for tracking cooldowns and determining when to spawn a new obstacle.

    private int level = 0; // The current difficulty level. The spawner changes its behaviour over time based on the player's score. Each level corresponds to a different behaviour.

    private float obstaclesDeadZone; // The x-coordinate beyond which an obstacle is considered out of the screen and should be destroyed. This will be calculated once with the SetObstacleDeadzone() method and passed to each obstacle upon spawn.
    private List<ObstacleBehaviour> obstacles = new List<ObstacleBehaviour>(); // A list of ObstacleBehavior references. Every time the spawner creates an obstacle, it keeps a reference to it for future communications.

    #region => SPAWNER SYSTEM COMPONENTS
    [Header("Difficulty Vectors")]
    /* SYSTEM MAIN PROCEDURE
     * Before looking at my solution, try to design the spawning procedure of the spawner yourself.
     * Remember: my solution is just one of many possible ones.
     * Since the next part can be difficult for those new to coding, the important thing is that you understand what I did more than how I did it.
     */
    #region ==>MY SOLUTION
    /* What are the difficulty vectors?
     * 1. Obstacle movement speed (the higher it is, the more difficult it becomes)
     * 2. Obstacle spawn rate (the closer the spawn of the obstacles, the more intense the gameplay becomes)
     * 3. Type of obstacles (the wider or taller the obstacles, the more precision they require during the jump).
     * 
     * There may be other design problems to consider. How do these dimensions relate to each other?
     * If the first two are independent, the third dimension is closely related to the first, but only for obstacles that develop in length.
     * If a very long obstacle can be overcome at high speeds, at low speeds it will require more precision, or it may even be impossible to jump over!
     * We might consider whether we really need obstacles developed in width (for the sake of variety) or not.
     */
    //
    // How to set these difficulty vectors?
    [Tooltip("Set Dino speed (value, on y) at Score (time, on x)")]
    public AnimationCurve speedProgressionCurve; // Check the Inspector. Double-clicking on the Animation Curve will open a two-dimensional graph.
                                                 // As the score increases, the system calculates the corresponding speed based on the curve.
    [System.Serializable] // The "Serializable" attribute (of the System library) allows this struct to be serialized. In Unity, serialized objects can be viewed and edited in the Inspector.
    public struct BehaviorController
    {
        [Tooltip("Score at which the conditions below are applied.")]
        public int scoreThreshold;
        [Tooltip("X: Multiplier. Y: Multiplier Swap Probability. The sum of all probabilities must be equal to 1.")]
        public Vector2[] multiplierSwapProbability;
        [Tooltip("This is the list from which the spawner draws obstacles.")]
        public List<GameObject> obstaclesPool;
    }
    /* This 'struct' I called BehaviorController is a collection of essential variables for managing the game's progression.
     * The struct asks for:
     * - a 'score threshold';
     * - a list of multipliers to apply to baseCooldown and corresponding probability that a given multiplier will be drawn. It determines after how much time a new obstacle will be generated;
     * - a list of obstacles from which the system can draw.
     */

    [Tooltip("Manage the spawner behaviour")]
    public BehaviorController[] controller;
    /* Then, I created an array of BehaviorControllers.
     * As you can see in the Inspector, an array is like a numbered list of elements. The first index of the array is 0.
     * We will use indexes to manage difficulty levels. At index zero there will be the BehaviorController of level zero, at index 1 the BehaviorController of level 1 and so on.
     * Please note: the score threshold of a BehaviorController must always be greater than that of the BehaviorController at the previous index!
     * (We will create a safety function that will alert us if there is an error.)
     *
     * Look at how everything is managed from the Inspector to understand how this tool works and the entire procedure.
     * I understand that it can be difficult, but this is the heart of the design of the entire project.
     */
    #endregion
    #endregion
    #endregion

    int GetScore()
    {
        return GameManager.instance.score;
    }

    float GetSpeed()
    {
        var _score = GetScore();
        var _speed = speedProgressionCurve.Evaluate(_score); // Evaluate returns the curve speed value (y) at corrisponding given score (x).
        return _speed;
    }
    /* Those functions have a return type.
     * The first one gets the score value (an integer) from the Game Manager.
     * The second one returns obstacle's actual 'speed' (based on the score) each time it's called.
     */

    #region INITIALIZATION
    void Start()
    {
        SetBaseCooldown();
        SetObstacleDeadzone();
        BehaviorControllerSecurityCheck();
    }

    #region INITIALIZATION METHODS
    void SetBaseCooldown()
    {
        // SETTING baseCooldown
        // Get the Player GameObject, then retrieve the CharacterBehavior component
        var _playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>();
        #region 🡹WTF ARE THAT?
        /* "var" is a keyword that allows you to declare a variable without explicitly specifying its data type.
         * The compiler determines the data type of the variable based on the type of the expression used to initialize it.
         * In this istance: var is CharacterBehavior variable.
         */
        #endregion
        #region WHY DECLARING A VARIABLE HERE?
        /* We declare this variable here because it is only needed within the context of the Start() method.
         * The playerBehavior variable will be created in the Start() method and then destroyed outside of the method.
         * That's why it is called "local variable".
         */
        #endregion
        // Calculate baseCooldown: 2f * (PlayerJumpMomentum / GravityAcceleration)
        baseCooldown = 2f * Mathf.Sqrt(2f * _playerBehavior.gravity * _playerBehavior.jumpHeight) / _playerBehavior.gravity;
        // Add baseCooldownAllowance to baseCooldown
        baseCooldown += baseCooldownAllowance; // Try to understand yourself why I used this allowance value. It's a game designer thing.
    }

    void SetObstacleDeadzone()
    {
        // Pick the main camera's transform
        var camTransform = Camera.main.transform;
        // Sets the dead zone on the opposite side of the screen, based on the spawner's horizontal position relative to the camera.
        obstaclesDeadZone = camTransform.position.x - (transform.position.x - camTransform.position.x);
    }

    void BehaviorControllerSecurityCheck()
    {
        int _lastThreshold = 0;
        for (int i = 0; i < controller.Length; i++)
        {
            if (_lastThreshold >= controller[i].scoreThreshold)
            {
                Debug.LogWarning("There's a design error in the Spawner Controller list");
                return;
            }
            else
            {
                _lastThreshold = controller[i].scoreThreshold;
            }
        }
    }
    #endregion
    #endregion
    #region UPDATE LOOP
    void Update()
    {
        timer += Time.deltaTime; // Tick update

        /* Then, we check if the elapsed time is greater than the "cooldown" value.
         * If it is, the spawner calls the function to instantiate a new obstacle.
         * This ensures that obstacles are spawned at regular intervals, controlled by the cooldown.
         */
        if (timer >= cooldown)
        {
            ResetTimer();
            SpawnNewObstacle();
            PickRandomCooldown();
        }
    }

    #region UPDATE METHODS
    void ResetTimer()
    {
        timer -= cooldown; // Why I subtract the cooldown value instead to set timer = 0f?
    }

    void SpawnNewObstacle()
    {
        var _pool = controller[level].obstaclesPool; // Pick the controller at index 'level' of the array, then pick the obstaclesPool.

        // Instantiate a random obstacle GameObject from the obstacleList.
        var _newObstacle = Instantiate(                 // Create a new GO, stored in a 'local variable' called '_newObstacle'.
            _pool[Random.Range(0, _pool.Count)],        // This GO is a random obstacle from the pool.
            transform.position, transform.rotation,     // Position and Rotation of the new GO (we choose the spawner's position and rotation).
            this.transform                              // Parent the instantiated obstacle to the movingGround GameObject's transform.
        );
        
        var _newObstacleBehavior = _newObstacle.GetComponent<ObstacleBehaviour>(); // '_newObstacle' is a GO. We need to access the ObstacleBehaviour component attached to it.
        obstacles.Add(_newObstacleBehavior); // Add ObstacleBehavior component of the newObstacle GO to the obstacles list.
                                             // In this way, we can call ObstacleBehavior public methods when we need to set the obstacle speed.
        var _speed = GetSpeed(); // GetSpeed() returns a value, do you remember?
        _newObstacleBehavior.ObstacleSetup(obstaclesDeadZone, _speed, this); // Now, we can call the ObstacleSetup() method inside this ObstacleBehaviour.
                                                                             // This method needs some input values to work.
    }

    public void DestroyObstacle(ObstacleBehaviour obstacleReference)
    {
        obstacles.Remove(obstacleReference); // Removes the obstacle from obstacles list.
        /* Try this.
         * 1. Make obstacle a public list. Now, you can check the list from the inspector.
         * 2. Comment the Remove function above.
         * 3. Play the game. Watch what happens when the obstacle is destroyed.
         * Now you know why we need to remove the obstacle from the list and why the console call missing reference during UpdateSpeedObstacles
         */
        Destroy(obstacleReference.gameObject); // NOTE: you have to destroy the GO, not the ObjstacleBehavior component attached!
    }

    public void UpdateObstaclesBehavior()
    {
        var _score = GetScore();
        UpdateSpeedObstacles(_score);
        EvaluateDifficultyLevelIncrease(_score);
    }

    void UpdateSpeedObstacles(float score)
    {
        var _speed = GetSpeed();

        foreach (var obstacle in obstacles) // foreach loop picks all the element of a list to solve its function
        {
            obstacle.SetNewSpeed(_speed); // ...like change the speed of each obstacle in the obstacles list.
        }
    }

    public void EvaluateDifficultyLevelIncrease(float score) //Called by the GameManager when the AddScore Event is invoked.
    {
        var _levelUpScoreThreshold = controller[level].scoreThreshold; // Pick score threshold on the list, using level as index

        if (score == _levelUpScoreThreshold) 
        {
            level++;                                    // level up!
            var _levelMax = controller.Length - 1;      // Level Max is the last index of the list. It is the list length minus 1.
            level = Mathf.Clamp(level, 0, _levelMax);   // level can't be higher of Level Max

            var _multiplierSwapProbability = controller[level].multiplierSwapProbability;

            // Check if probabilities are = 1. You don't need to understand this code.
            float probabilitySum = 0f;
            for (int i = 0; i < _multiplierSwapProbability.Length; i++)  
            {
                probabilitySum += _multiplierSwapProbability[i].y;
            }
            for (int i = 0; i < _multiplierSwapProbability.Length; i++)
            {
                _multiplierSwapProbability[i].y /= probabilitySum;
            }            
        }
    }

    public void PickRandomCooldown()
    {
        float random = Random.Range(0f, 1f); // Pick a random float number, 0 to 1. 
        float cumulativeProbability = 0f;

        var _multiplierSwapProbability = controller[level].multiplierSwapProbability; // Pick the list.

        for (int i = 0; i < _multiplierSwapProbability.Length; i++) // This is a for loop. A for loop is a control flow statement that allows code to be executed a certain number of time.
                                                                    // It has three components: initialization, condition, and iteration.
                                                                    // Starting from index 0 to an index minor of the list length (5), do the stuff. Then, increment index 'i' by 1.
        {
            cumulativeProbability += _multiplierSwapProbability[i].y;       // Add the multiplayer at index 'i' to cumulativeProbability.
            if (random < cumulativeProbability)                             // Is the random picked number less than cumulativeProbility?
            {
                cooldown = baseCooldown * _multiplierSwapProbability[i].x;  // True: set the new cooldown...
                return;                                                     // ...and then stops the for loop. We don't need it anymore.
                                                                            // Otherwise, continue the loop. Increment the index 'i' by 1, check if its minor of the list length, interate the code.
            }
        }
    }
    #endregion
    #endregion
}
