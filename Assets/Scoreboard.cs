using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int mazeValue = 10;
    [SerializeField] ColorNames lastFilter = ColorNames.White;
    [SerializeField] int freeFilterShifts = 3;
    [SerializeField] int penalty = 0;
    [SerializeField] int penaltyMultiplier = 1;

    ColorShifter shifter;

    private void Awake()
    {
        shifter = FindObjectOfType<ColorShifter>();
    }

    public void UpdateScore()
    {
        score += mazeValue - penalty;
    }

    public void LoadLevelValues(Level level)
    {
        mazeValue = level.cellColumns * level.cellRows;
        freeFilterShifts = level.mazeClearLayers.Count;
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
        if (shifter.RActive && shifter.GActive && shifter.BActive)
        {
            return ColorNames.White;
        } else if (shifter.RActive && !shifter.GActive && !shifter.BActive)
        {
            return ColorNames.Red;
        } else if (!shifter.RActive && shifter.GActive && !shifter.BActive)
        {
            return ColorNames.Green;
        } else if (!shifter.RActive && !shifter.GActive && shifter.BActive)
        {
            return ColorNames.Blue;
        } else if (shifter.RActive && shifter.GActive && !shifter.BActive)
        {
            return ColorNames.Yellow;
        } else if (shifter.RActive && !shifter.GActive && shifter.BActive)
        {
            return ColorNames.Violet;
        } else if (!shifter.RActive && shifter.GActive && shifter.BActive)
        {
            return ColorNames.Cyan;
        } else
        {
            return ColorNames.Black;
        }
    }

}
