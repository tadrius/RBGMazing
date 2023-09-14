using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColor : MonoBehaviour
{
    [SerializeField] bool r = true, g = true, b = true;
    [SerializeField] bool includeInactiveColors = false;

    SpriteRenderer spriteRenderer;
    ColorChannelActivator colorChannelActivator;

    public bool R { get { return r; } set { r = value; } }
    public bool G { get { return g; } set { g = value; } }
    public bool B { get { return b; } set { b = value; } }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorChannelActivator = FindObjectOfType<ColorChannelActivator>();
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
            rAmount = colorChannelActivator.RActive && r ? 1f : 0f;
            gAmount = colorChannelActivator.GActive && g ? 1f : 0f;
            bAmount = colorChannelActivator.BActive && b ? 1f : 0f;
        }
        spriteRenderer.color = new Color(rAmount, gAmount, bAmount);
    }

    public int GetMatchingColorCount(SpriteColor other)
    {
        int count = 0;

        // ignore colors that are inactive in color channels
        if (colorChannelActivator.RActive && r && other.R) { count++; }
        if (colorChannelActivator.GActive && g && other.G) { count++; }
        if (colorChannelActivator.BActive && b && other.B) { count++; }

        return count;
    }

}
