using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class IMGUIDLG : MonoBehaviour, IInteractable
{
    //Should be private/ serialise field
    public string[] linesOfDlg = new string[0];
    public int lineIndex = 0;
    public string characterName = "";
    public bool showDlg = false;

    #region CAN BE USED FOR CANVAS
    public void Interaction()
    {
        GameManager.instance.ChangeState(GameManager.GameStates.Menu);
        lineIndex = 0;
        showDlg = true;
        //change game state to menu
    }
    public void Next()
    {
        lineIndex++;
    }
    public void Exit()
    {
        lineIndex = 0;
        showDlg = false;
        //change game state to playing game
        GameManager.instance.ChangeState(GameManager.GameStates.Playing);

    }
    #endregion
    #region IMGUI EXAMPLE ONLY
    Rect UIPos(float startX, float startY, float sizeX, float sizeY)
    {
        return new Rect(startX * (Screen.width / 16), startY * (Screen.height / 9), sizeX * (Screen.width / 16), sizeY * (Screen.height / 9));
    }
    private void Start()
    {
        //Debug.Log(linesOfDlg.Length);
        //Debug.Log("Verses");
        //for (int i = 0; i < linesOfDlg.Length; i++)
        //{
        //    Debug.Log(i+1);
        //}

        //Debug.Log(linesOfDlg.Length-1);
        //Debug.Log("Verses");
        //for (int i = 0; i < linesOfDlg.Length; i++)
        //{
        //    Debug.Log(i);
        //}
    }
    void OnGUI()
    {
        if (showDlg)
        {
            //GUI Box that covers bottom 3rd of screen that displays our currnt line of DLG
            //GUI.Box(UIPos(0, 6, 16, 3), linesOfDlg[lineIndex]);
            //GUI.Box(UIPos(0, 6, 16, 3), characterName + ": " + linesOfDlg[lineIndex]);
            GUI.Box(UIPos(0, 6, 16, 3), $"{characterName}: {linesOfDlg[lineIndex]}");
            //be able to move through the lines if its not the last line
            //are we not at the last line??
            if (lineIndex < linesOfDlg.Length-1)
            {
                //go to next
                if (GUI.Button(UIPos(14.25f,6.25f,1.5f,0.5f), "Next"))
                {
                    //lineIndex++;
                    //lineIndex += 1;
                    //lineIndex = lineIndex + 1;
                    Next();
                }
            }
            //if it is the last line we need to end conversation 
            else
            {
                //bye
                if (GUI.Button(UIPos(14.25f, 6.25f, 1.5f, 0.5f), "Bye!"))
                {
                   // lineIndex = 0;
                   // showDlg = false;
                   Exit();
                }
            }
        }
    }
    #endregion
}
