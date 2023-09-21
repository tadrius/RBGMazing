using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] [Range(0.00001f, 2f)] float mazeScreenProportion = 1f;
    [SerializeField] float mazeScaleConstant = 1f;
    [SerializeField] float mazeYOffset = 0f;

    ColorPicker colorPicker;
    LevelManager levelManager;
    Maze maze;
    Scoreboard scoreboard;

    Level currentLevel;

    private void Awake()
    {
        maze = FindObjectOfType<Maze>();
        colorPicker = FindObjectOfType<ColorPicker>();
        levelManager = GetComponent<LevelManager>();
        scoreboard = GetComponent<Scoreboard>();
    }

    private void Start()
    {
        currentLevel = levelManager.GetCurrentLevel();
        maze.Generate(currentLevel);
        scoreboard.LoadLevelValues(currentLevel);
    }

    private void Update()
    {
        RescaleMaze();
        RepositionMaze();
        if (MazeComplete())
        {
            // evaluate the current maze
            scoreboard.UpdateScore();

            // prepare the next maze
            levelManager.IncrementLevelIndex();
            currentLevel = levelManager.GetCurrentLevel();
            maze.Generate(levelManager.GetCurrentLevel());
            colorPicker.Reset();
            scoreboard.LoadLevelValues(currentLevel);
        }
    }

    bool MazeComplete()
    {
        return maze.GoalReached();
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
/*        Debug.Log($"Screen Width: {Screen.currentResolution.width}, Height: {Screen.currentResolution.height}, Safe Width: {Screen.safeArea.width}, Safe Height: {Screen.safeArea.height}");
*/
        // TODO - incorporate safe area for mobile devices
        float mazeScale = mazeScreenProportion * mazeScaleConstant / Screen.currentResolution.height;

        maze.transform.localScale = new Vector3(mazeScale, mazeScale, mazeScale);
    }

}
