using UnityEngine;
using System.Collections;

public static class InputManager {

	public static bool Right
    {
        get { return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow); }
    }

    public static bool Left
    {
        get { return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow); }
    }

    public static bool Jump
    {
        get { return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow); }
    }

    public static bool Action
    {
        get { return Input.GetKeyDown(KeyCode.Space); }
    }

}
