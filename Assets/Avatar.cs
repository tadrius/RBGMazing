using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    Vector2Int coordinates;
    Mover mover;
    SpriteColor color;

    public Vector2Int Coordinates { get { return coordinates; } set { coordinates = value; } }
    public Mover Mover { get { return mover; } }
    public SpriteColor Color { get { return color; } }

    private void Awake()
    {
        mover = GetComponent<Mover>();
        color = GetComponent<SpriteColor>();
    }

}
