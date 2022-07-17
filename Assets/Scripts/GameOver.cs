using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private TextMeshProUGUI GameOverText;
    void Start()
    {
        GameOverText = GetComponent<TextMeshProUGUI>();
        GameOverText.text = $"HighScore{ScoreCounter.Score}";
    }
    
}
