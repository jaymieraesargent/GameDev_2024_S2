using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public void ChangeCursorImage(Texture2D cursor)
    {
        Cursor.SetCursor(cursor,Vector2.zero, CursorMode.Auto);
    }
}
