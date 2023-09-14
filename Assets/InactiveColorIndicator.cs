using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveColorIndicator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    ColorChannelActivator colorChannels;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorChannels = FindObjectOfType<ColorChannelActivator>();
    }
}
