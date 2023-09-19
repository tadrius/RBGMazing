using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PathClearChannels {

    // When drawing maze paths, the following fields determine which color channels are deactivated in walls along the path.
    public bool red;
    public bool green;
    public bool blue;

    public PathClearChannels(bool red, bool green, bool blue)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }
}
