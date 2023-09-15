using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] Vector2Int startPosition;
    [SerializeField] Vector2Int endPosition;

    [SerializeField] Avatar avatar;
    [SerializeField] GameObject goalIndicator;

    CellGrid grid;

    public CellGrid Grid { get { return grid; } }

    private void Awake()
    {
        grid = GetComponentInChildren<CellGrid>();
    }

    public void Generate(Level level)
    {
        grid.Generate(level.cellColumns, level.cellRows);

        foreach (ColorChannels mazeLayer in level.mazeClearLayers)
        {
            DrawMaze(mazeLayer);
        }

        avatar.Coordinates = startPosition;
        avatar.transform.position = grid.GetCellPositionFromCoordinates(avatar.Coordinates);
        goalIndicator.transform.position = grid.GetCellPositionFromCoordinates(endPosition);
    }

    // using Wilson's algorithm (https://en.wikipedia.org/wiki/Maze_generation_algorithm)
    // pathClearChannels determines which color channels will be removed from walls along paths
    void DrawMaze(ColorChannels pathClearChannels)
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

    List<Cell> CreatePath(List<Cell> remainingCells, List<Cell> mazeCells, ColorChannels pathClearChannels)
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

    void ClearPathWalls(List<Cell> path, ColorChannels pathClearChannels)
    {
        // clear walls
        for (int i = 0; i < path.Count - 1; i++)
        {
            Wall wall = grid.GetAdjoiningWall(path[i], path[i + 1]);
            SpriteColor wallColor = wall.MainColor;

            wall.SetAllColors(
                wallColor.R && !pathClearChannels.red,
                wallColor.G && !pathClearChannels.green,
                wallColor.B && !pathClearChannels.blue
            );
        }
    }

    List<Vector2Int> CreateDirections()
    {
        return new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    }
}
