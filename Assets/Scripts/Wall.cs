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
        SetAllColors(mainColor.RedActive, mainColor.GreenActive, mainColor.BlueActive);
    }

    private void Update()
    {
        SetAllColors(mainColor.RedActive, mainColor.GreenActive, mainColor.BlueActive);
    }

    public void SetAllColors(bool r, bool g, bool b)
    {
        foreach (var item in allColors)
        {
            item.SetColorsActive(r, g, b);
        }
    }

    public void SetName(Vector2Int coordinates)
    {
        name = $"{name} {coordinates}";
    }
}
