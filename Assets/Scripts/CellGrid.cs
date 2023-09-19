using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{

    [SerializeField] int cols = 10;
    [SerializeField] int rows = 10;
    [SerializeField] float cellScale = 1f;

    [SerializeField] Cell cellPrefab;
    [SerializeField] Wall horizontalWallPrefab;
    [SerializeField] Wall verticalWallPrefab;

    Dictionary<Vector2, Cell> cellsByCoordinates;
    Dictionary<Vector2, Wall> horizontalWallsByCoordinates;
    Dictionary<Vector2, Wall> verticalWallsByCoordinates;

    List<Cell> cells;
    List<Wall> walls;

    public Dictionary<Vector2, Cell> CellsByCoordinates { get { return cellsByCoordinates; } }
    public List<Cell> Cells { get {  return cells; } }

    public void Generate(int cols, int rows)
    {
        // reuse the existing grid if the size is the same
        if ((this.cols == cols) && (this.rows == rows) && (cells.Count > 0)) {
            // reset all walls
            ResetWalls();
            return; 
        }

        // otherwise clear the existing grid and create a new one
        Clear();

        this.cols = cols;
        this.rows = rows;
        CreateWalls();
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                CreateCell(x, y);
            }
        }
    }

    void ResetWalls()
    {
        foreach(Wall wall in walls)
        {
            wall.MainColor.SetColorsActive(true, true, true);
        }
    }

    void Clear()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        cells = new();
        walls = new();
        cellsByCoordinates = new();
        horizontalWallsByCoordinates = new();
        verticalWallsByCoordinates = new();
    }

    void CreateCell(int x, int y)
    {
        Cell cell = Instantiate(cellPrefab, new Vector3(
            x * cellScale * transform.lossyScale.x,
            y * cellScale * transform.lossyScale.x,
            0), Quaternion.identity, transform);
        cell.transform.localScale *= cellScale;

        AssignCellWalls(cell, x, y);

        cells.Add(cell);
        Vector2Int coordinates = new (x, y);
        cell.SetCoordinates(coordinates);
        cellsByCoordinates.Add(coordinates, cell);
    }

    void AssignCellWalls(Cell cell, int x, int y)
    {
        Wall leftWall = GetVerticalWallByCoordinates(x, y);
        Wall rightWall = GetVerticalWallByCoordinates(x + 1, y);
        Wall bottomWall = GetHorizontalWallByCoordinates(x, y);
        Wall topWall = GetHorizontalWallByCoordinates(x, y + 1);

        cell.SetWall(leftWall, Side.Left);
        cell.SetWall(rightWall, Side.Right);
        cell.SetWall(bottomWall, Side.Bottom);
        cell.SetWall(topWall, Side.Top);
    }

    void CreateWalls()
    {
        // vertical walls
        for (int x = 0; x < cols + 1; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Wall wall = Instantiate(verticalWallPrefab, new Vector3(
                    (x - .5f)   * cellScale * transform.lossyScale.x,
                    (y)         * cellScale * transform.lossyScale.x,
                    0), Quaternion.identity, transform);
                wall.transform.localScale *= cellScale;

                Vector2Int coordinates = new (x, y);
                wall.SetName(coordinates);
                verticalWallsByCoordinates.Add(coordinates, wall);
                walls.Add(wall);
            }
        }

        // horizontal walls
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows + 1; y++)
            {
                Wall wall = Instantiate(horizontalWallPrefab, new Vector3(
                    (x)         * cellScale * transform.lossyScale.x,
                    (y - .5f)   * cellScale * transform.lossyScale.x,
                    0), Quaternion.identity, transform);
                wall.transform.localScale *= cellScale;

                Vector2Int coordinates = new (x, y);
                wall.SetName(coordinates);
                horizontalWallsByCoordinates.Add(coordinates, wall);
                walls.Add(wall);
            }
        }
    }

    Wall GetVerticalWallByCoordinates(int x, int y)
    {
        Vector2Int coordinates = new (x, y);
        if (verticalWallsByCoordinates.ContainsKey(coordinates))
        {
            return verticalWallsByCoordinates[coordinates];
        }
        return null;
    }

    Wall GetHorizontalWallByCoordinates(int x, int y)
    {
        Vector2Int coordinates = new (x, y);
        if (horizontalWallsByCoordinates.ContainsKey(coordinates))
        {
            return horizontalWallsByCoordinates[coordinates];
        }
        return null;
    }

    public Wall GetAdjoiningWall(Cell cell1, Cell cell2)
    {
        Vector2Int direction = cell2.Coordinates - cell1.Coordinates;
        if (direction == Vector2Int.up)
        {
            return cell1.GetWall(Side.Top);
        } else if (direction == Vector2Int.down)
        {
            return cell1.GetWall(Side.Bottom);
        } else if (direction == Vector2Int.left)
        {
            return cell1.GetWall(Side.Left);
        } else
        {
            return cell1.GetWall(Side.Right);
        }
    }

    public bool ContainsCellCoordinates(Vector2Int coordinates)
    {
        return CellsByCoordinates.ContainsKey(coordinates);
    }

    public Cell GetCellFromCoordinates(Vector2Int coordinates)
    {
        return CellsByCoordinates[coordinates];
    }

    public Vector3 GetCellPositionFromCoordinates(Vector2Int coordinates)
    {
        return GetCellFromCoordinates(coordinates).transform.position;
    }

}
