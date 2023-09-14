using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoBehaviour
{

    [SerializeField] float mazeYOffset = 0f;
    
    Maze maze;

    private void Awake()
    {
        maze = FindObjectOfType<Maze>();
    }

    private void Start()
    {
        maze.Generate();
    }

    private void Update()
    {
        RepositionMaze();
    }

    void RepositionMaze()
    {
        Vector3 gridLocalCenter =
            (maze.Grid.Cells[0].transform.localPosition
                + maze.Grid.Cells[^1].transform.localPosition) 
                / 2;
        maze.transform.position = (-gridLocalCenter * maze.transform.localScale.x) + new Vector3(0f, mazeYOffset);
    }
}
