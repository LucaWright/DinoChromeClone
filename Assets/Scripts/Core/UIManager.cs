using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;

    public void UpdateScore()
    {
        scoreText.text = GameManager.instance.score.ToString();
    }

}
