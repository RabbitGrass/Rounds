using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMannager : MonoBehaviour
{
    public Texture2D cursorIcon;
    void Start()
    {
        Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto);
    }

}
