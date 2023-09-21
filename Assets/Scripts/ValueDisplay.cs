using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text ActiveFilterText;
    [SerializeField] TMP_Text MovePenaltyText;
    [SerializeField] TMP_Text mazeNumberText;
    [SerializeField] TMP_Text mazeValueText;

    Scoreboard scoreboard;
    LevelManager levelManager;

    private void Awake()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        UpdateScore();
        UpdateActiveFilter();
        UpdateMovePenalty();
        UpdateMazeNumber();
        UpdateMazeValue();
    }

    void UpdateScore()
    {
        scoreText.text = $"{scoreboard.Score}";
    }

    void UpdateActiveFilter()
    {
        if (scoreboard.ActiveFilter == scoreboard.BufferFilter)
        {
            ActiveFilterText.text = $"{scoreboard.ActiveFilter}";
        } else
        {
            ActiveFilterText.text = $"{scoreboard.ActiveFilter} (>{scoreboard.BufferFilter})";
        }
    }

    void UpdateMovePenalty()
    {
        MovePenaltyText.text = $"{scoreboard.MovePenalty}";
        if (scoreboard.ActiveFilter == scoreboard.BufferFilter)
        {
            MovePenaltyText.text = $"{scoreboard.MovePenalty}";
        }
        else
        {
            MovePenaltyText.text = $"{scoreboard.MovePenalty} (+1)";
        }
    }

    void UpdateMazeNumber()
    {
        mazeNumberText.text = $"{levelManager.LevelIndex}";
    }

    void UpdateMazeValue()
    {
        if (scoreboard.TotalPenalty == 0)
        {
            mazeValueText.text = $"{scoreboard.MazeValue}";

        }
        else
        {
            mazeValueText.text = $"{scoreboard.MazeValue} (-{scoreboard.TotalPenalty})";
        }
    }
}
