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

        avatar.Coordinates = startPosition;
        avatar.transform.position = grid.GetCellPositionFromCoordinates(avatar.Coordinates);
        goalIndicator.transform.position = grid.GetCellPositionFromCoordinates(endPosition);
    }
}
