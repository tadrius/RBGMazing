using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] Vector2Int startPosition;
    [SerializeField] Vector2Int endPosition;

    [SerializeField] Avatar avatar;
    [SerializeField] GameObject goalIndicator;

    CellGrid grid;

    private void Awake()
    {
        grid = GetComponent<CellGrid>();
    }

    public void Generate()
    {
        grid.Generate();
        CreateMaze();

        avatar.Coordinates = startPosition;
        avatar.transform.position = grid.GetCellPositionFromCoordinates(avatar.Coordinates);
        goalIndicator.transform.position = grid.GetCellPositionFromCoordinates(endPosition);
    }

    // using Wilson's algorithm (https://en.wikipedia.org/wiki/Maze_generation_algorithm)
    void CreateMaze()
    {
        List<Cell> remainingCells = new (grid.Cells);
        List<Cell> mazeCells = new ();

        int index = Random.Range(0, remainingCells.Count);
        Cell cell = remainingCells[index];
        remainingCells.RemoveAt(index);
        mazeCells.Add(cell);

        while (remainingCells.Count > 0)
        {
            mazeCells.AddRange(CreatePath(remainingCells, mazeCells));
        }
    }

    List<Cell> CreatePath(List<Cell> remainingCells, List<Cell> mazeCells)
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

        // clear walls
        for (int i = 0; i < path.Count - 1; i++)
        {
            Wall wall = grid.GetAdjoiningWall(path[i], path[i + 1]);
            wall.SetAllColors(false, false, false);
        }

        // remove from remaining cells
        foreach (Cell pathCell in path)
        {
            remainingCells.Remove(pathCell);
        }

        // return the path
        return path;
    }

    List<Vector2Int> CreateDirections()
    {
        return new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    }
}
