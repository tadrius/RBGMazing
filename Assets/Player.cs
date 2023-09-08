using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    PlayerActions actions;
    Avatar avatar;
    ColorPicker colorPicker;
    Camera mainCamera;

    private void Awake()
    {
        actions = GetComponent<PlayerActions>();
        avatar = GetComponentInChildren<Avatar>();
        colorPicker = FindObjectOfType<ColorPicker>();
        mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (actions.Moved)
        {
            actions.Moved = false;
            HandleMove();
            //Debug.Log($"Moved - x: {actions.MoveVector.x}, y: {actions.MoveVector.y}");
        }
        if (actions.Clicked)
        {
            actions.Clicked = false;
            //Debug.Log($"Clicked - down: {actions.ClickerDown}");
            HandleClick();
        }
        if (actions.Pointed)
        {
            actions.Pointed = false;
            //Debug.Log($"Pointed - x: {pointVector.x}, y: {pointVector.y}");
            HandlePoint();
        }
    }

    void HandleMove()
    {
        avatar.Mover.Move(actions.MoveVector);
    }

    void HandleClick()
    {
        if (actions.ClickerDown)
        {
            colorPicker.enabled = true;
            HandlePoint();
        }
        else
        {
            colorPicker.enabled = false;
        }
    }

    void HandlePoint()
    {
        if (colorPicker.enabled)
        {
            Vector2 pointVector = mainCamera.ScreenToWorldPoint(actions.PointVector);
            colorPicker.PickColor(pointVector);
        }
    }

}
