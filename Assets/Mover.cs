using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Avatar avatar;
    CellGrid grid;

    private void Awake()
    {
        avatar = GetComponent<Avatar>();
        grid = FindObjectOfType<CellGrid>();
    }

    public void Move(Vector2 direction)
    {
        int yMove = 0, xMove = 0;

        // can only move along 1 axis at a time
        if (direction.x > 0) // right
        {
            xMove++;
        } else if (direction.x < 0) // left
        {
            xMove--;
        } else if (direction.y > 0) // up
        {
            yMove++;
        } else if (direction.y < 0) // down
        {
            yMove--;
        }

        // return if no direction is indicated
        if (yMove == 0 && xMove == 0) { return; }

        // return if the current tile is invalid (matches colors with the avatar)
        if (avatar.GetMatchingColorCount(grid.CellsByCoordinates[avatar.Coordinates]) > 0) { return; }

        // check if the adjacent tile in the indicated direction is a valid destination (does not match colors with the avatar)
        Vector2Int destinationCoordinates = new Vector2Int(avatar.Coordinates.x + xMove, avatar.Coordinates.y + yMove);
        
        if (grid.CellsByCoordinates.ContainsKey(destinationCoordinates) 
            && avatar.GetMatchingColorCount(grid.CellsByCoordinates[destinationCoordinates]) <= 0)
        {
            
            // find the furthest valid destination in the indicated direction
            Vector2Int candidateCoordinates = new Vector2Int(destinationCoordinates.x + xMove, destinationCoordinates.y + yMove);
            while (grid.CellsByCoordinates.ContainsKey(candidateCoordinates) 
                && avatar.GetMatchingColorCount(grid.CellsByCoordinates[candidateCoordinates]) <= 0)
            {
                destinationCoordinates = new Vector2Int(candidateCoordinates.x, candidateCoordinates.y);
                candidateCoordinates += new Vector2Int(xMove, yMove);
            }

            // move to the furthest valid destination 
            transform.position = grid.GetCellPositionFromCoordinates(destinationCoordinates);
            avatar.Coordinates = destinationCoordinates;
        }
    }
}
