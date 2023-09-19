using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] [Range(0.0001f, 2f)] float mazeScreenProportion = 1f;
    [SerializeField] float mazeScaleConstant = 1f;
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
        RescaleMaze();
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

    void RescaleMaze()
    {
        // TODO - delete log
        Debug.Log($"Screen Width: {Screen.currentResolution.width}, Height: {Screen.currentResolution.height}, Safe Width: {Screen.safeArea.width}, Safe Height: {Screen.safeArea.height}");

        // TODO - incorporate safe area for mobile devices
        float mazeScale = mazeScreenProportion * mazeScaleConstant / Screen.currentResolution.height;

        maze.transform.localScale = new Vector3(mazeScale, mazeScale, mazeScale);
    }

}
