using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ColorChannels {
    public bool red;
    public bool green;
    public bool blue;

    public ColorChannels(bool red, bool green, bool blue)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }
}
