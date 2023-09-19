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

    private void Awake()
    {
        globalColors = FindObjectsOfType<SpriteColor>();
    }

    void ActivateColorChannels(bool rActive, bool gActive, bool bActive)
    {
        this.rActive = rActive;
        this.gActive = gActive;
        this.bActive = bActive;
        foreach (var color in globalColors)
        {
            color.ApplyColorChannels();
        }
    }

    public void ActivateR()
    {
        ActivateColorChannels(true, false, false);
    }

    public void ActivateRG()
    {
        ActivateColorChannels(true, true, false);
    }

    public void ActivateG()
    {
        ActivateColorChannels(false, true, false);
    }

    public void ActivateGB()
    {
        ActivateColorChannels(false, true, true);
    }

    public void ActivateB()
    {
        ActivateColorChannels(false, false, true);
    }

    public void ActivateBR()
    {
        ActivateColorChannels(true, false, true);
    }

    public void ActivateRGB()
    {
        ActivateColorChannels(true, true, true);
    }
}
