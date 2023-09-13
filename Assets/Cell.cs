using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    readonly Dictionary<Side, Wall> wallsBySide = new Dictionary<Side, Wall>();

    // TODO - delete after testing
    [SerializeField] Wall topWall;
    [SerializeField] Wall bottomWall;
    [SerializeField] Wall leftWall;
    [SerializeField] Wall rightWall;


    public void SetWall(Wall wall, Side side)
    {
        if (wall == null) return;
        wallsBySide[side] = wall;

        // TODO - delete after testing
        if (side  == Side.Left)
        {
            leftWall = wall;
        } else if (side == Side.Right)
        {
            rightWall = wall;
        } else if (side == Side.Top)
        {
            topWall = wall;
        } else
        {
            bottomWall = wall;
        }

    }

    public Wall GetWall(Side side)
    {
        return wallsBySide[side];
    }

}
