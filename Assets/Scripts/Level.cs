using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public int cellColumns;
    public int cellRows;
    [Range(1, 10)] public int numberOfGoals = 1;
    public List<PathClearChannels> mazeLayers;
}
