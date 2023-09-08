using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    PlayerActions actions;
    Avatar avatar;
    ColorPicker colorPicker;

    private void Awake()
    {
        actions = GetComponent<PlayerActions>();
        avatar = GetComponentInChildren<Avatar>();
    }

    private void Update()
    {
        if (actions.Moved)
        {
            Debug.Log($"Moved - x: {actions.MoveVector.x}, y: {actions.MoveVector.y}");
            avatar.Mover.Move(actions.MoveVector);
            actions.Moved = false;
        }
        if (actions.Clicked)
        {
            Debug.Log($"Clicked - down: {actions.ClickerDown}");
            actions.Clicked = false;
        }
        if (actions.Pointed)
        {
            Debug.Log($"Pointed - x: {actions.PointVector.x}, y: {actions.PointVector.y}");
            actions.Pointed = false;
        }
    }

}
