using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] Vector2Int startPosition;

    [SerializeField] Avatar avatar;
    [SerializeField] GameObject goalIndicator;

    CellGrid grid;
    Dictionary<Vector2Int, GameObject> goalsByCoordinates;

    public CellGrid Grid { get { return grid; } }

    private void Awake()
    {
        grid = GetComponentInChildren<CellGrid>();
    }

    public void Generate(Level level)
    {
        grid.Generate(level.cellColumns, level.cellRows);

        foreach (PathClearChannels mazeLayer in level.mazeLayers)
        {
            DrawMaze(mazeLayer);
        }

        avatar.transform.position = grid.GetCellPositionFromCoordinates(avatar.Coordinates);
        AddGoals(level.numberOfGoals);
    }

    // TODO - create object pool for goals
    void AddGoals(int numberOfGoals)
    {
        List<Cell> cells = new(grid.Cells);
        goalsByCoordinates = new();
        cells.Remove(grid.GetCellFromCoordinates(avatar.Coordinates));
        for (int i = 0; i < numberOfGoals; i++)
        {
            if (cells.Count == 0) { return; }

            int index = Random.Range(0, cells.Count);
            Cell cell = cells[index];
            goalsByCoordinates.Add(cell.Coordinates, Instantiate(goalIndicator, cell.transform.position, Quaternion.identity, transform));
            cells.RemoveAt(index);
        }
    }

    public bool GoalAtCoordinates(Vector2Int coordinates)
    {
        return goalsByCoordinates.ContainsKey(coordinates);
    }

    // TODO - create object pool for goals
    public void RemoveGoalAtCoordinates(Vector2Int coordinates)
    {
        GameObject goal = goalsByCoordinates[coordinates];
        goalsByCoordinates.Remove(coordinates);
        Destroy(goal);
    }

    // using Wilson's algorithm (https://en.wikipedia.org/wiki/Maze_generation_algorithm)
    // pathClearChannels determines which color channels will be removed from walls along paths
    void DrawMaze(PathClearChannels pathClearChannels)
    {
        List<Cell> remainingCells = new (grid.Cells);
        List<Cell> mazeCells = new ();

        int index = Random.Range(0, remainingCells.Count);
        Cell cell = remainingCells[index];
        remainingCells.RemoveAt(index);
        mazeCells.Add(cell);

        while (remainingCells.Count > 0)
        {
            mazeCells.AddRange(CreatePath(remainingCells, mazeCells, pathClearChannels));
        }
    }

    List<Cell> CreatePath(List<Cell> remainingCells, List<Cell> mazeCells, PathClearChannels pathClearChannels)
    {
        // start the path at an arbitrary cell
        List<Cell> path = new();
        int index = Random.Range(0, remainingCells.Count);
        Cell cell = remainingCells[index];
        path.Add(cell);

        // until the path reaches a cell in the maze
        while (!mazeCells.Contains(cell))
        {
            // walk in a random direction
            List<Vector2Int> directions = CreateDirections();
            Vector2Int direction = directions[Random.Range(0, directions.Count)];
            Vector2Int candidateCoordinates = cell.Coordinates + direction;

            while (!grid.ContainsCellCoordinates(candidateCoordinates))
            {
                direction = directions[Random.Range(0, directions.Count)];
                candidateCoordinates = cell.Coordinates + direction;
            }

            Cell nextCell = grid.GetCellFromCoordinates(candidateCoordinates);

            // if the walk reaches the path, a loop forms; remove the loop before continuing
            if (path.Contains(nextCell))
            {
                int loopStartIndex = path.IndexOf(nextCell);
                path.RemoveRange(loopStartIndex, path.Count - loopStartIndex);
            }
            path.Add(nextCell);
            cell = nextCell;
        }


        ClearPathWalls(path, pathClearChannels);

        // remove from remaining cells
        foreach (Cell pathCell in path)
        {
            remainingCells.Remove(pathCell);
        }

        // return the path
        return path;
    }

    void ClearPathWalls(List<Cell> path, PathClearChannels pathClearChannels)
    {
        // clear walls
        for (int i = 0; i < path.Count - 1; i++)
        {
            Wall wall = grid.GetAdjoiningWall(path[i], path[i + 1]);
            SpriteColor wallColor = wall.MainColor;

            wall.SetAllColors(
                wallColor.RedActive && !pathClearChannels.red,
                wallColor.GreenActive && !pathClearChannels.green,
                wallColor.BlueActive && !pathClearChannels.blue
            );
        }
    }

    List<Vector2Int> CreateDirections()
    {
        return new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    }

    public bool GoalsCleared()
    {
        if (goalsByCoordinates.Count == 0) return true;
        return false;
    }
}
