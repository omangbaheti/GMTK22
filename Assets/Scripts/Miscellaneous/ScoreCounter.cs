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
    
    private int score = 0;

    public int Score
    {
        get { return score; }
    }

    void Start()
    {
        scoreBar = GetComponent<TextMeshProUGUI>();
        scoreBar.text = score.ToString();
        IncrementScore += IncreaseScore;
    }

    private void IncreaseScore()
    {
        score++;
        scoreBar.text = score.ToString();
    }

    private void OnDestroy()
    {
        IncrementScore -= IncreaseScore;
    }
}
