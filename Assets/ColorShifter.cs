using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShifter : MonoBehaviour
{

    [SerializeField] bool rActive = true, gActive = true, bActive = true;
    bool rActiveOld, gActiveOld, bActiveOld;

    public bool RActive { get { return rActive; } }
    public bool GActive { get { return gActive; } }
    public bool BActive { get { return bActive; } }

    SpriteColor[] colors;

    private void Awake()
    {
        colors = FindObjectsOfType<SpriteColor>();
    }

    private void Start()
    {
        UpdateOldActiveColors();
    }

    // Update is called once per frame
    void Update()
    {
        // update all colors if there is a change in active color channels
        if ((rActiveOld != rActive) || (gActiveOld != gActive) || (bActiveOld != bActive))
        {
            UpdateOldActiveColors();
            foreach (var color in colors)
            {
                color.ApplyColorChannels();
            }
        }
    }

    void UpdateOldActiveColors()
    {
        rActiveOld = rActive;
        gActiveOld = gActive;
        bActiveOld = bActive;
    }

    void ActivateColorChannels(bool rActive, bool gActive, bool bActive)
    {
        this.rActive = rActive;
        this.gActive = gActive;
        this.bActive = bActive;
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
