using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColor : MonoBehaviour
{
    bool rActive, gActive, bActive;

    SpriteRenderer spriteRenderer;
    ColorChannels colorChannels;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorChannels = FindObjectOfType<ColorChannels>();
    }

    public void ApplyColors(bool rActive, bool gActive, bool bActive)
    {
        this.rActive = rActive;
        this.gActive = gActive;
        this.bActive = bActive;

        ApplyColorChannels();
    }

    public void ApplyColorChannels()
    {
        float r, g, b;

        r = colorChannels.RActive && rActive ? 1f : 0f;
        g = colorChannels.GActive && gActive ? 1f : 0f;
        b = colorChannels.BActive && bActive ? 1f : 0f;

        spriteRenderer.color = new Color(r, g, b);
    }
}
