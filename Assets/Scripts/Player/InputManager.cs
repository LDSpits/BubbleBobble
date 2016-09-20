﻿using UnityEngine;
using System.Collections;

public static class InputManager {

    public static bool Right(Players.player player)
    {
        if(Players.player1 == player)
            return Input.GetKey(KeyCode.D);

        return Input.GetKey(KeyCode.RightArrow);
    }

    public static bool Left(Players.player player)
    {
        if (Players.player1 == player)
            return Input.GetKey(KeyCode.A);

        return Input.GetKey(KeyCode.LeftArrow);
    }

    public static bool Jump(Players.player player)
    {
        if (Players.player1 == player)
            return Input.GetKey(KeyCode.W);

        return Input.GetKey(KeyCode.UpArrow);
    }

    public static bool Action(Players.player player)
    {
        if (Players.player1 == player)
            return Input.GetKey(KeyCode.Space);

        return Input.GetKey(KeyCode.RightControl);
    }

}
