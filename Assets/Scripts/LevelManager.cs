using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] int currentLevel = 0;
    [SerializeField] List<Level> levels;
    [Tooltip("Beyond level will define all maze beyond the defined levels.")]
    [SerializeField] Level beyondLevel;

    public void Restart()
    {
        currentLevel = 0;
    }

    public void IncreaseCurrentLevel()
    {
        currentLevel++;
    }

    public Level GetCurrentLevel()
    {
        if (currentLevel >= levels.Count)
        {
            return beyondLevel;
        } else
        {
            return levels[currentLevel];
        }
    }

}
