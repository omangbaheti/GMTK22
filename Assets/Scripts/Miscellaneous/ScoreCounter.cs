using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static Action IncrementScore;
    [SerializeField] private TextMeshProUGUI scoreBar;
    
    private static int score = 0;

    public static int Score
    {
        get { return score; }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        scoreBar = GetComponent<TextMeshProUGUI>();
        scoreBar.text = score.ToString();
        IncrementScore += IncreaseScore;
    }

    private void IncreaseScore()
    {
        score++;
        scoreBar.text = $"Score: {score.ToString()}";
    }

    private void OnDestroy()
    {
        IncrementScore -= IncreaseScore;
    }
}
