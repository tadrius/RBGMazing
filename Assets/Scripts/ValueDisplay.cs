using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text valueText;
    [SerializeField] TMP_Text mazeNumberText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text lastFilterText;
    [SerializeField] TMP_Text penaltyText;
    [SerializeField] TMP_Text multiplierText;

    Scoreboard scoreboard;
    LevelManager levelManager;

    private void Awake()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        valueText.text = $"Value: {scoreboard.MazeValue}";
        mazeNumberText.text = $"Maze: {levelManager.LevelIndex}";
        scoreText.text = $"Score: {scoreboard.Score}";
        lastFilterText.text = $"Last Filter: {scoreboard.LastFilter}";
        penaltyText.text = $"Penalty: {scoreboard.Penalty}";
        multiplierText.text = $"Multiplier: {scoreboard.PenaltyMultiplier}";
    }
}
