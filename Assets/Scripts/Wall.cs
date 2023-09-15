using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Wall : MonoBehaviour
{
    [SerializeField] SpriteColor mainColor;

    SpriteColor[] allColors;

    public SpriteColor MainColor { get { return mainColor; } }

    private void Awake()
    {
        allColors = GetComponentsInChildren<SpriteColor>();
    }

    private void Start()
    {
        SetAllColors(mainColor.R, mainColor.G, mainColor.B);
    }

    // TO-DO - remove Update after testing (colors only need updating when Maze is made)
    private void Update()
    {
        SetAllColors(mainColor.R, mainColor.G, mainColor.B);
    }

    public void SetName(Vector2Int coordinates)
    {
        name = $"{name} {coordinates}";
    }

    public void SetAllColors(bool r, bool g, bool b)
    {
        foreach (var item in allColors)
        {
            item.ApplyColors(r, g, b);
        }
    }
}
