using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int mazeValue = 10;
    [SerializeField] int penalty = 0;
    [SerializeField] int penaltyMultiplier = 1;
    [SerializeField] int freeFilterShifts = 3;
    [SerializeField] ColorNames lastFilter = ColorNames.White;

    ColorFilter filter;

    public int Score { get { return score; } }
    public int MazeValue { get {  return mazeValue; } }
    public int Penalty { get { return penalty; } }
    public int PenaltyMultiplier { get {  return penaltyMultiplier; } }
    public int FreeFilterShifts { get { return freeFilterShifts; } }
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
        freeFilterShifts = level.mazeLayers.Count;
        lastFilter = GetColorFilterName();
        penalty = 0;
        penaltyMultiplier = 1;
    }

    public void OnAvatarMove()
    {
        penalty += penaltyMultiplier;

        ColorNames currentFilter = GetColorFilterName();
        if (lastFilter != currentFilter)
        {
            lastFilter = currentFilter;
            if (freeFilterShifts <= 0)
            {
                penaltyMultiplier++;
            } else
            {
                freeFilterShifts = Mathf.Max(freeFilterShifts - 1, 0);
            }
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
