using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] float mazeYOffset = 0f;
    
    LevelManager levelManager;
    Maze maze;

    private void Awake()
    {
        maze = FindObjectOfType<Maze>();
        levelManager = GetComponent<LevelManager>();
    }

    private void Start()
    {
        maze.Generate(levelManager.GetCurrentLevel());
    }

    private void Update()
    {
        RepositionMaze();
    }

    void RepositionMaze()
    {
        if (maze.Grid.Cells.Count <= 0) { return; }
        Vector3 gridLocalCenter =
            (maze.Grid.Cells[0].transform.localPosition
                + maze.Grid.Cells[^1].transform.localPosition) 
                / 2;
        maze.transform.position = (-gridLocalCenter * maze.transform.localScale.x) + new Vector3(0f, mazeYOffset);
    }
}
