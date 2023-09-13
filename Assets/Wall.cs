using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] bool r = true, g = true, b = true;
    [SerializeField] List<SpriteColor> inactiveColorIndicator = new ();

    SpriteColor[] colors;

    public bool R { get { return r; } set { r = value; } }
    public bool G { get { return g; } set { g = value; } }
    public bool B { get { return b; } set { b = value; } }

    private void Awake()
    {
        colors = GetComponentsInChildren<SpriteColor>();
    }

    private void Start()
    {
        foreach(var item in colors)
        {
            item.ApplyColors(r, g, b);
        }
    }

    // TO-DO - remove Update
    private void Update()
    {
        foreach (var item in colors)
        {
            item.ApplyColors(r, g, b);
        }
    }

    public void SetColor(bool r, bool g, bool b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }

    public void SetRandomColor()
    {
        r = Random.Range(0, 2) == 1;
        g = Random.Range(0, 2) == 1;
        b = Random.Range(0, 2) == 1;
    }
}
