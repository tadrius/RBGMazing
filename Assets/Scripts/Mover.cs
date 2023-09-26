using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Avatar avatar;
    Maze maze;
    Scoreboard scoreboard;

    private void Awake()
    {
        avatar = GetComponent<Avatar>();
        maze = FindObjectOfType<Maze>();
        scoreboard = FindObjectOfType<Scoreboard>();
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

        // determine the destination coordinates
        Vector2Int destinationCoordinates = new (avatar.Coordinates.x + xMove, avatar.Coordinates.y + yMove);
        
        // if the coordinates have a corresponding cell and the path is not blocked
        if (maze.Grid.CellsByCoordinates.ContainsKey(destinationCoordinates) 
            && !PathIsBlocked(avatar, destinationCoordinates))
        {
            // move to the destination 
            transform.position = maze.Grid.GetCellPositionFromCoordinates(destinationCoordinates);
            avatar.Coordinates = destinationCoordinates;

            // check for goal
            if (maze.GoalAtCoordinates(avatar.Coordinates))
            {
                maze.RemoveGoalAtCoordinates(avatar.Coordinates);
            }

            // update scoreboard
            scoreboard.OnAvatarMove();
        }
    }

    bool PathIsBlocked(Avatar avatar, Vector2Int destinationCoordinates)
    {
        Wall wall = maze.Grid.GetAdjoiningWall(
            maze.Grid.GetCellFromCoordinates(avatar.Coordinates),
            maze.Grid.GetCellFromCoordinates(destinationCoordinates));
        return 0 < avatar.Color.CountMatchingColorChannels(wall.MainColor);
    }
}
