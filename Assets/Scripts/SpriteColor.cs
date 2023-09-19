using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColor : MonoBehaviour
{
    [SerializeField] bool redActive = true, greenActive = true, blueActive = true;
    [SerializeField] bool includeInactiveColors = false;

    SpriteRenderer spriteRenderer;
    ColorFilter colorFilter;

    public bool RedActive { get { return redActive; } }
    public bool GreenActive { get { return greenActive; } }
    public bool BlueActive { get { return blueActive; } }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorFilter = FindObjectOfType<ColorFilter>();
    }

    public void SetColorsActive(bool redActive, bool greenActive, bool blueActive)
    {
        this.redActive = redActive;
        this.greenActive = greenActive;
        this.blueActive = blueActive;

        ApplyColorChannels();
    }

    public void ApplyColorChannels()
    {
        float rAmount, gAmount, bAmount;

        if (includeInactiveColors)
        {
            rAmount = redActive ? 1f : 0f;
            gAmount = greenActive ? 1f : 0f;
            bAmount = blueActive ? 1f : 0f;
        }
        else
        { 
            rAmount = colorFilter.RActive && redActive ? 1f : 0f;
            gAmount = colorFilter.GActive && greenActive ? 1f : 0f;
            bAmount = colorFilter.BActive && blueActive ? 1f : 0f;
        }
        spriteRenderer.color = new Color(rAmount, gAmount, bAmount);
    }

    public int CountMatchingColorChannels(SpriteColor other)
    {
        int count = 0;

        // ignore colors that are inactive in color channels
        if (colorFilter.RActive && redActive && other.RedActive) { count++; }
        if (colorFilter.GActive && greenActive && other.GreenActive) { count++; }
        if (colorFilter.BActive && blueActive && other.BlueActive) { count++; }

        return count;
    }

}
