using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColor : MonoBehaviour
{
    [SerializeField] bool r = true, g = true, b = true;
    [SerializeField] bool includeInactiveColors = false;

    SpriteRenderer spriteRenderer;
    ColorShifter colorShifter;

    public bool R { get { return r; } set { r = value; } }
    public bool G { get { return g; } set { g = value; } }
    public bool B { get { return b; } set { b = value; } }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorShifter = FindObjectOfType<ColorShifter>();
    }

    public void ApplyColors(bool r, bool g, bool b)
    {
        this.r = r;
        this.g = g;
        this.b = b;

        ApplyColorChannels();
    }

    public void ApplyColorChannels()
    {
        float rAmount, gAmount, bAmount;

        if (includeInactiveColors)
        {
            rAmount = r ? 1f : 0f;
            gAmount = g ? 1f : 0f;
            bAmount = b ? 1f : 0f;
        }
        else
        { 
            rAmount = colorShifter.RActive && r ? 1f : 0f;
            gAmount = colorShifter.GActive && g ? 1f : 0f;
            bAmount = colorShifter.BActive && b ? 1f : 0f;
        }
        spriteRenderer.color = new Color(rAmount, gAmount, bAmount);
    }

    public int GetMatchingColorCount(SpriteColor other)
    {
        int count = 0;

        // ignore colors that are inactive in color channels
        if (colorShifter.RActive && r && other.R) { count++; }
        if (colorShifter.GActive && g && other.G) { count++; }
        if (colorShifter.BActive && b && other.B) { count++; }

        return count;
    }

}
