using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool r = true, g = true, b = true;
    [SerializeField] SpriteColor inactiveColorIndicator;

    SpriteColor color;

    public bool R { get { return r; } set { r = value; } }
    public bool G { get { return g; } set { g = value; } }
    public bool B { get { return b; } set { b = value; } }

    private void Awake()
    {
        color = GetComponent<SpriteColor>();
    }

    private void Start()
    {
        color.ApplyColors(r, g, b);
        inactiveColorIndicator.ApplyColors(r, g, b);
    }

    // TO-DO - remove Update
    private void Update()
    {
        color.ApplyColors(r, g, b);
        inactiveColorIndicator.ApplyColors(r, g, b);
    }

    public void SetRandomColor()
    {
        r = Random.Range(0, 2) == 1;
        g = Random.Range(0, 2) == 1;
        b = Random.Range(0, 2) == 1;
    }
}
