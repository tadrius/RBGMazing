using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    PlayerActions actions;
    Avatar avatar;

    private void Awake()
    {
        actions = GetComponent<PlayerActions>();
        avatar = GetComponentInChildren<Avatar>();
    }

    private void Update()
    {
        if (actions.moved)
        {
            avatar.Mover.Move(actions.moveVector);
            actions.moved = false;
        }
        if (actions.clicked)
        {
            actions.clicked = false;
        }
    }

}
