using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFilter : MonoBehaviour
{

    [SerializeField] bool rActive = true, gActive = true, bActive = true;

    public bool RActive { get { return rActive; } }
    public bool GActive { get { return gActive; } }
    public bool BActive { get { return bActive; } }

    SpriteColor[] globalColors;
    Scoreboard scoreboard;

    private void Awake()
    {
        globalColors = FindObjectsOfType<SpriteColor>();
        scoreboard = FindObjectOfType<Scoreboard>();
    }

    void FilterColors(bool rActive, bool gActive, bool bActive)
    {
        this.rActive = rActive;
        this.gActive = gActive;
        this.bActive = bActive;
        foreach (var color in globalColors)
        {
            color.ApplyColorChannels();
        }
        scoreboard.OnFilterColors();
    }

    public void ActivateR()
    {
        FilterColors(true, false, false);
    }

    public void ActivateRG()
    {
        FilterColors(true, true, false);
    }

    public void ActivateG()
    {
        FilterColors(false, true, false);
    }

    public void ActivateGB()
    {
        FilterColors(false, true, true);
    }

    public void ActivateB()
    {
        FilterColors(false, false, true);
    }

    public void ActivateBR()
    {
        FilterColors(true, false, true);
    }

    public void ActivateRGB()
    {
        FilterColors(true, true, true);
    }
}
