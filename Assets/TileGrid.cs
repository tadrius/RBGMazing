using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{

    [SerializeField] int cols = 10;
    [SerializeField] int rows = 10;
    [SerializeField] float tileGap = .1f;
    [SerializeField] float scale = 1f;
    [SerializeField] Tile tilePrefab;

    Dictionary<Vector2, Tile> tilesByCoordinates;
    List<Tile> tiles;

    public Dictionary<Vector2, Tile> TilesByCoordinates { get { return tilesByCoordinates; } }
    public List<Tile> Tiles { get {  return tiles; } }

    public void Generate()
    {
        tilesByCoordinates = new Dictionary<Vector2, Tile>();
        tiles = new List<Tile>();

        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Tile tile = Instantiate(tilePrefab, new Vector3(
                    (x + (x * tileGap)) * scale, 
                    (y + (y * tileGap)) * scale, 
                    0), Quaternion.identity, transform);
                tile.transform.localScale *= scale;

                tiles.Add(tile);

                Vector2Int coordinates = new Vector2Int(x, y);
                tilesByCoordinates.Add(coordinates, tile);
            }
        }
    }

    public Tile GetTileFromCoordinates(Vector2Int coordinates)
    {
        return TilesByCoordinates[coordinates];
    }

    public Vector3 GetTilePositionFromCoordinates(Vector2Int coordinates)
    {
        return GetTileFromCoordinates(coordinates).transform.position;
    }

}
