using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int mazeValue = 10;
    [SerializeField] int totalPenalty = 0;
    [SerializeField] int movePenalty = 0;
    [SerializeField] ColorNames activeFilter = ColorNames.White;
    [SerializeField] ColorNames bufferFilter;

    ColorFilter filter;

    public int Score { get { return score; } }
    public int MazeValue { get {  return mazeValue; } }
    public int TotalPenalty { get { return totalPenalty; } }
    public int MovePenalty { get {  return movePenalty; } }
    public ColorNames ActiveFilter { get { return activeFilter; } }
    public ColorNames BufferFilter { get { return bufferFilter; } }

    private void Awake()
    {
        filter = FindObjectOfType<ColorFilter>();
    }

    public void UpdateScore()
    {
        score += mazeValue - totalPenalty;
    }

    public void LoadLevelValues(Level level)
    {
        mazeValue = level.cellColumns * level.cellRows;
        activeFilter = GetColorFilterName();
        bufferFilter = GetColorFilterName();
        totalPenalty = 0;
        movePenalty = 0;
    }

    public void OnFilterColors()
    {
        bufferFilter = GetColorFilterName();
    }

    public void OnAvatarMove()
    {
        // increase penalty up to the value of the maze
        totalPenalty = Mathf.Min(mazeValue, totalPenalty + movePenalty);

        if (activeFilter != bufferFilter)
        {
            activeFilter = bufferFilter;
            movePenalty++;
        }
    }

    ColorNames GetColorFilterName()
    {
        if (filter.RActive && filter.GActive && filter.BActive)
        {
            return ColorNames.White;
        } else if (filter.RActive && !filter.GActive && !filter.BActive)
        {
            return ColorNames.Red;
        } else if (!filter.RActive && filter.GActive && !filter.BActive)
        {
            return ColorNames.Green;
        } else if (!filter.RActive && !filter.GActive && filter.BActive)
        {
            return ColorNames.Blue;
        } else if (filter.RActive && filter.GActive && !filter.BActive)
        {
            return ColorNames.Yellow;
        } else if (filter.RActive && !filter.GActive && filter.BActive)
        {
            return ColorNames.Violet;
        } else if (!filter.RActive && filter.GActive && filter.BActive)
        {
            return ColorNames.Cyan;
        } else
        {
            return ColorNames.Black;
        }
    }

}
