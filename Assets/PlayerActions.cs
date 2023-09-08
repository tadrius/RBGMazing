using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{

    bool clicked;
    bool moved;
    bool pointed;


    bool clickerDown;
    Vector2 moveVector;
    Vector2 pointVector;

    public bool Clicked { get { return clicked; } set { clicked = value; } }
    public bool Moved { get { return moved; } set { moved = value; } }
    public bool Pointed { get { return pointed; } set { pointed = value; } }

    public bool ClickerDown { get { return clickerDown; } }
    public Vector2 MoveVector { get { return moveVector; } }
    public Vector2 PointVector { get { return pointVector; } }

    private void Awake()
    {
        clickerDown = false;
    }

    void OnMove(InputValue value)
    {
        moved = true;
        moveVector = value.Get<Vector2>();
    }

    void OnClick()
    {
        clicked = true;
        clickerDown = !clickerDown;
    }

    void OnPoint(InputValue value)
    {
        pointed = true;
        pointVector = value.Get<Vector2>();
    }
}
