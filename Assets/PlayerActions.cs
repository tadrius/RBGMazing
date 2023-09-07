using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{

    public bool moved;
    public bool clicked;

    public Vector2 moveVector; 

    void OnMove(InputValue value)
    {
        moved = true;
        moveVector = value.Get<Vector2>();
    }

    void OnClick()
    {
        clicked = true;
    }
}
