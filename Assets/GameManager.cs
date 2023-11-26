using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score;
    int highScore;

    public CharacterBehavior player;
    public SpawnerBehaviour spawner;
    public MovingGroundBehavior movingGround;
    public Text scoreText;

    [Space]
    [Header("Difficulty vectors")]
    [Tooltip("Set Dino speed (value) at Score (Time)")]
    public AnimationCurve progressionCurve;
    [Tooltip("Set spawn rates by score")]
    public SpawnerCooldownController[] spawnerCooldownController;
    int level = 0;

    public UnityEvent GameOverEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called by TriggerBox in Player GO
    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
        EvaluateSpawnerLevelUp();
        movingGround.speed = progressionCurve.Evaluate(score);
    }

    void EvaluateSpawnerLevelUp()
    {
        if (score == spawnerCooldownController[level].scoreLimit)
        {
            level++;
            level = Mathf.Clamp(level, 0, spawnerCooldownController.Length - 1);

            // Check if probabilities are = 1
            float probabilitySum = 0f;
            for (int i = 0; i < spawnerCooldownController[level].cooldownMultiplyer_Probability.Length; i++)
            {
                probabilitySum += spawnerCooldownController[level].cooldownMultiplyer_Probability[1].y;
            }
            for (int i = 0; i < spawnerCooldownController[level].cooldownMultiplyer_Probability.Length; i++)
            {
                spawnerCooldownController[level].cooldownMultiplyer_Probability[1].y /= probabilitySum;
            }
        }
    }

    public void PickRandomCooldown()
    {
        float random = Random.Range(0f, 1f);
        float cumulativeProbability = 0f;

        for (int i = 0; i < spawnerCooldownController[level].cooldownMultiplyer_Probability.Length; i++)
        {
            cumulativeProbability += spawnerCooldownController[level].cooldownMultiplyer_Probability[i].y;
            if (random < cumulativeProbability)
            {
                spawner.cooldown = spawner.baseCooldown * spawnerCooldownController[level].cooldownMultiplyer_Probability[i].x;
                Debug.Log("Pescato valore all'indice: " + i);
                return;
            }         
        }
    }

    public void GameOver()
    {
        GameOverEvent.Invoke();
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        EditorSceneManager.LoadScene(0);
        Time.timeScale = 1f;

    }
}
