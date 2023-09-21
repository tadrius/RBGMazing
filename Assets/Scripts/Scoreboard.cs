using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int mazeValue = 10;
    [SerializeField] int totalPenalty = 0;
    [SerializeField] int movePenalty = 0;
    [SerializeField] int movePenaltyIncrease = 0;
    [SerializeField] ColorNames activeFilter = ColorNames.White;
    [SerializeField] ColorNames bufferFilter;

    [SerializeField] int penaltyChangeAll = 0;
    [SerializeField] int penaltyChangeSecondary = 1;
    [SerializeField] int penaltyChangePrimary = 2;

    ColorFilter filter;

    public int Score { get { return score; } }
    public int MazeValue { get {  return mazeValue; } }
    public int TotalPenalty { get { return totalPenalty; } }
    public int MovePenalty { get {  return movePenalty; } }
    public int MovePenaltyIncrease { get { return movePenaltyIncrease; } }
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
        GetColorFilterValues(out activeFilter, out int x);
        GetColorFilterValues(out bufferFilter, out movePenaltyIncrease);
        totalPenalty = 0;
        movePenalty = 0;
    }

    public void OnFilterColors()
    {
        GetColorFilterValues(out bufferFilter, out movePenaltyIncrease);
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

    void GetColorFilterValues(out ColorNames name, out int movePenaltyIncrease)
    {
        if (filter.RActive && !filter.GActive && !filter.BActive)
        {
            name = ColorNames.Red;
            movePenaltyIncrease = penaltyChangePrimary;
        } else if (!filter.RActive && filter.GActive && !filter.BActive)
        {
            name = ColorNames.Green;
            movePenaltyIncrease = penaltyChangePrimary;
        } else if (!filter.RActive && !filter.GActive && filter.BActive)
        {
            name = ColorNames.Blue;
            movePenaltyIncrease = penaltyChangePrimary;
        } else if (filter.RActive && filter.GActive && !filter.BActive)
        {
            name = ColorNames.Yellow;
            movePenaltyIncrease = penaltyChangeSecondary;
        } else if (filter.RActive && !filter.GActive && filter.BActive)
        {
            name = ColorNames.Violet;
            movePenaltyIncrease = penaltyChangeSecondary;
        } else if (!filter.RActive && filter.GActive && filter.BActive)
        {
            name = ColorNames.Cyan;
            movePenaltyIncrease = penaltyChangeSecondary;
        }
        else
        {
            name = ColorNames.White;
            movePenaltyIncrease = penaltyChangeAll;
        }
    }

}
