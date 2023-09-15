using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChannels {
    public readonly bool red;
    public readonly bool green;
    public readonly bool blue;

    public ColorChannels(bool red, bool green, bool blue)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }
}
