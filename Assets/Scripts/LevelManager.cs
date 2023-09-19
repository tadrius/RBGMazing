using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] int levelIndex = 0;
    [SerializeField] List<Level> levels;
    [Tooltip("Beyond level will define all maze beyond the defined levels.")]
    [SerializeField] Level beyondLevel;

    public int LevelIndex { get { return levelIndex; } }

    public void Restart()
    {
        levelIndex = 0;
    }

    public void IncrementLevelIndex()
    {
        levelIndex++;
    }

    public Level GetCurrentLevel()
    {
        if (levelIndex >= levels.Count)
        {
            return beyondLevel;
        } else
        {
            return levels[levelIndex];
        }
    }

}
