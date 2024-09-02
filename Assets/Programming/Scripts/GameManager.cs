using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //awake connect it 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this);
        }
        ChangeState(GameStates.Playing);
    }
    public enum GameStates
    {
        Playing,
        Paused,
        Menu,
        Death
    }
    public GameStates gameState;

    public void ChangeState(GameStates state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameStates.Playing:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameStates.Paused:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameStates.Menu:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            case GameStates.Death:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            default:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}
