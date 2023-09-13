using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell : MonoBehaviour
{
    readonly Dictionary<Side, Wall> wallsBySide = new Dictionary<Side, Wall>();

    Vector2Int coordinates;

    public Vector2Int Coordinates { get { return coordinates; } }

    public void SetCoordinates(Vector2Int coordinates)
    {
        this.coordinates = coordinates;
        name = $"{name} {coordinates}";
    }

    public void SetWall(Wall wall, Side side)
    {
        if (wall == null) return;
        wallsBySide[side] = wall;
    }

    public Wall GetWall(Side side)
    {
        if (wallsBySide.ContainsKey(side)) return wallsBySide[side];
        return null;
    }

    public List<Wall> GetWalls()
    {
        return wallsBySide.Values.ToList();
    }

}
