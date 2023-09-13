using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{

    [SerializeField] int cols = 10;
    [SerializeField] int rows = 10;
    [SerializeField] float scale = 1f;

    [SerializeField] Cell cellPrefab;
    [SerializeField] Wall horizontalWallPrefab;
    [SerializeField] Wall verticalWallPrefab;

    Dictionary<Vector2, Cell> cellsByCoordinates;
    Dictionary<Vector2, Wall> horizontalWallsByCoordinates;
    Dictionary<Vector2, Wall> verticalWallsByCoordinates;

    List<Cell> cells;

    public Dictionary<Vector2, Cell> CellsByCoordinates { get { return cellsByCoordinates; } }
    public List<Cell> Cells { get {  return cells; } }

    public void Generate()
    {
        CreateWalls();

        cellsByCoordinates = new Dictionary<Vector2, Cell>();
        cells = new List<Cell>();

        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                CreateCell(x, y);
            }
        }
    }

    void CreateCell(int x, int y)
    {
        Cell cell = Instantiate(cellPrefab, new Vector3(
            x * scale,
            y * scale,
            0), Quaternion.identity, transform);
        cell.transform.localScale *= scale;

        AssignCellWalls(cell, x, y);

        cells.Add(cell);
        Vector2Int coordinates = new (x, y);
        cell.SetCoordinates(coordinates);
        cellsByCoordinates.Add(coordinates, cell);
    }

    void AssignCellWalls(Cell cell, int x, int y)
    {
        Wall leftWall = GetVerticalWallByCoordinates(x - 1, y);
        Wall rightWall = GetVerticalWallByCoordinates(x, y);
        Wall bottomWall = GetHorizontalWallByCoordinates(x, y - 1);
        Wall topWall = GetHorizontalWallByCoordinates(x, y);

        cell.SetWall(leftWall, Side.Left);
        cell.SetWall(rightWall, Side.Right);
        cell.SetWall(bottomWall, Side.Bottom);
        cell.SetWall(topWall, Side.Top);
    }

    void CreateWalls()
    {
        // vertical walls
        verticalWallsByCoordinates = new Dictionary<Vector2, Wall>();
        for (int x = 0; x < cols - 1; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Wall wall = Instantiate(verticalWallPrefab, new Vector3(
                    (x + .5f) * scale,
                    (y) * scale,
                    0), Quaternion.identity, transform);
                wall.transform.localScale *= scale;

                Vector2Int coordinates = new (x, y);
                wall.SetName(coordinates);
                verticalWallsByCoordinates.Add(coordinates, wall);
            }
        }

        // horizontal walls
        horizontalWallsByCoordinates = new Dictionary<Vector2, Wall>();
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows - 1; y++)
            {
                Wall wall = Instantiate(horizontalWallPrefab, new Vector3(
                    (x) * scale,
                    (y + .5f) * scale,
                    0), Quaternion.identity, transform);
                wall.transform.localScale *= scale;

                Vector2Int coordinates = new (x, y);
                wall.SetName(coordinates);
                horizontalWallsByCoordinates.Add(coordinates, wall);
            }
        }
    }

    Wall GetVerticalWallByCoordinates(int x, int y)
    {
        Vector2Int coordinates = new Vector2Int(x, y);
        if (verticalWallsByCoordinates.ContainsKey(coordinates))
        {
            return verticalWallsByCoordinates[coordinates];
        }
        return null;
    }

    Wall GetHorizontalWallByCoordinates(int x, int y)
    {
        Vector2Int coordinates = new Vector2Int(x, y);
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
