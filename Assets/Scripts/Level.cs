using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public int cellColumns;
    public int cellRows;
    public List<PathClearChannels> mazeLayers;
}
