using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerBehaviour : MonoBehaviour
{
    public GameObject movingGround;
    public List<GameObject> obstacleList = new List<GameObject>();
    public float baseCooldown = .5f;
    public float cooldown = .5f;
    public float tolerance = .1f;
    private float timer;

    public UnityEvent OnPickRandomCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        /* Set minCooldown: 2f*(PlayerJumpMomentum / GravityAcceleration) + tolerance */
        // Get the Player GO, then get the CharacterBehavior component
        var playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehavior>();
        // Calcultate minCooldown
        baseCooldown = 2f* Mathf.Sqrt(2f * playerBehaviour.gravity * playerBehaviour.jumpHeight) / playerBehaviour.gravity + tolerance;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            // Istantiate a random obstacle GO in obstacleList (choosing a random index in the list, from 0 to last index excluded),
            // than parent to this Go transform
            Instantiate(obstacleList[Random.Range(0, obstacleList.Count)], transform.position, transform.rotation, movingGround.transform);

            // Reset Timer
            timer -= cooldown;

            OnPickRandomCooldown.Invoke();
        }
    }
}
