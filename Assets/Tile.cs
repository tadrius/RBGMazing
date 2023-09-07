using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool r = true, g = true, b = true;

    SpriteColor color;

    public bool R { get { return r; } }
    public bool G { get { return g; } }
    public bool B { get { return b; } }

    private void Awake()
    {
        color = GetComponent<SpriteColor>();
    }

    private void Start()
    {
        color.ApplyColors(r, g, b);
    }

    // TO-DO - remove Update
    private void Update()
    {
        color.ApplyColors(r, g, b);
    }
}
