using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoBehaviour
{
    [SerializeField] float cameraYOffset = 0f;

    CellGrid tileGrid;
    Maze maze;
    Camera mainCamera;

    private void Awake()
    {
        tileGrid = FindObjectOfType<CellGrid>();
        maze = FindObjectOfType<Maze>();
        mainCamera = FindObjectOfType<Camera>();
    }

    private void Start()
    {
        maze.Generate();
        Vector3 gridCenter = 
            (tileGrid.Cells[0].transform.position 
            + tileGrid.Cells[tileGrid.Cells.Count - 1].transform.position) / 2;
        mainCamera.transform.position = new Vector3(
            gridCenter.x, 
            gridCenter.y + cameraYOffset, 
            mainCamera.transform.position.z); ;
    }
}
