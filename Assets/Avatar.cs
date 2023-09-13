using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] bool r = true, g = true, b = true;

    Vector2Int coordinates;
    Mover mover;
    SpriteColor color;
    ColorChannels colorChannels;

    public Vector2Int Coordinates { get { return coordinates; } set { coordinates = value; } }
    public Mover Mover { get { return mover; } }

    private void Awake()
    {
        mover = GetComponent<Mover>();
        color = GetComponent<SpriteColor>();
        colorChannels = FindObjectOfType<ColorChannels>();
    }

    // TO-DO - remove Update
    private void Update()
    {
        color.ApplyColors(r, g, b);
    }

    public int GetMatchingColorCount(Cell tile)
    {
        int count = 0;
        
/*        // ignore colors that are inactive in color channels
        if (colorChannels.RActive && r && tile.R) { count++; }
        if (colorChannels.GActive && g && tile.G) { count++; }
        if (colorChannels.BActive && b && tile.B) { count++; }*/
        
        return count;
    }


}
