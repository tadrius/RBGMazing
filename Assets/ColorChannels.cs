using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChannels : MonoBehaviour
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
}
