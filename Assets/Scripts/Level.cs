using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public int cellColumns;
    public int cellRows;
    public List<ColorChannels> mazeClearLayers; // each item corresponds to the color channels cleared to draw each layer's paths
}
