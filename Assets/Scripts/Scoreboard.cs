using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int mazeValue = 10;
    [SerializeField] int penalty = 0;
    [SerializeField] int penaltyMultiplier = 1;
    [SerializeField] ColorNames lastFilter = ColorNames.White;

    ColorFilter filter;

    public int Score { get { return score; } }
    public int MazeValue { get {  return mazeValue; } }
    public int Penalty { get { return penalty; } }
    public int PenaltyMultiplier { get {  return penaltyMultiplier; } }
    public ColorNames LastFilter { get { return lastFilter; } }

    private void Awake()
    {
        filter = FindObjectOfType<ColorFilter>();
    }

    public void UpdateScore()
    {
        score += mazeValue - penalty;
    }

    public void LoadLevelValues(Level level)
    {
        mazeValue = level.cellColumns * level.cellRows;
        lastFilter = GetColorFilterName();
        penalty = 0;
        penaltyMultiplier = 1;
    }

    public void OnAvatarMove()
    {
        // increase penalty up to the value of the maze
        penalty = Mathf.Min(mazeValue, penalty + penaltyMultiplier);

        ColorNames currentFilter = GetColorFilterName();
        if (lastFilter != currentFilter)
        {
            lastFilter = currentFilter;
            penaltyMultiplier++;
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
