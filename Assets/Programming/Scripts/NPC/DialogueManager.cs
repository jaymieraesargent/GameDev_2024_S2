using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    //there can be only one!!!
    public static DialogueManager instance;
    private void Awake()
    {
        //if its empty
        if (instance == null)
        {
            //fill the empty spot
            instance = this;
        }
        //if the instance reference has something there and its not the reference 
        else if (instance != null && instance != this)
        {
            //Destroy the imposter
            Destroy(this);
        }
    }
    #endregion
    #region Dialogue UI Variables
    [SerializeField] GameObject _dialogueBox;//what we turn on and off
    [SerializeField] Text _dialogueText;//the text box
    [SerializeField] Image _displayPicture;//the Display Picture
    [SerializeField] Text _name;//Characters name
    [SerializeField] Text _input;//Button we Edit
    [SerializeField] GameObject _noButton;//what we turn on and off

    #endregion
    #region Dialogue Variables
    //lines to read
    [SerializeField] string[] dialogueLines;
    //current line
    [SerializeField] int currentIndex = 0;
    [SerializeField] int questionIndex = -1;
    #region Approval
    [SerializeField] ApprovalDialogueLines _approvalDialogueLines;
    [SerializeField] DialogueApproval _currentApproval;
    #endregion
    #endregion

    public void OnActive(string[] lines, string name, Sprite dp)
    {
        _dialogueBox.SetActive(true);
        _noButton.SetActive(false);

        dialogueLines = lines;
        currentIndex = 0;
        _input.text = "Next";
        if (lines.Length <= 1)
        {
            _input.text = "Bye!";
        }
        _displayPicture.sprite = dp;
        _name.text = name;

        GameManager.instance.ChangeState(GameManager.GameStates.Menu);
        _dialogueText.text = dialogueLines[currentIndex];
        questionIndex = -1;
    }
    public void OnActive(string[] lines, string name, Sprite dp, int index)
    {
        _dialogueBox.SetActive(true);
        _noButton.SetActive(false);

        dialogueLines = lines;
        currentIndex = 0;
        _input.text = "Next";
        if (lines.Length <= 1)
        {
            _input.text = "Bye!";
        }
        _displayPicture.sprite = dp;
        _name.text = name;
        questionIndex = index;

        GameManager.instance.ChangeState(GameManager.GameStates.Menu);
        _dialogueText.text = dialogueLines[currentIndex];
    }
    public void OnActive(ApprovalDialogueLines lines, string name, Sprite dp, int index, DialogueApproval approval)
    {
        _dialogueBox.SetActive(true);
        _noButton.SetActive(false);

        _approvalDialogueLines = lines;
        currentIndex = 0;
        _input.text = "Next";
        if (lines.neutralLines.Length <= 1)
        {
            _input.text = "Bye!";
        }
        _displayPicture.sprite = dp;
        _name.text = name;
        questionIndex = index;
        _currentApproval = approval;
        GameManager.instance.ChangeState(GameManager.GameStates.Menu);
        //We need a Switch Statement Function to select the correct shiz
        //that will go here
        ChangeApproval();
        _dialogueText.text = dialogueLines[currentIndex];
    }
    void OnDeActive()
    {
        _dialogueBox.SetActive(false);
        _noButton.SetActive(false);
        _currentApproval = null;
        GameManager.instance.ChangeState(GameManager.GameStates.Playing);
    }
    void ChangeApproval()
    {
        if(_currentApproval == null)
        {
            return;
        }
        switch (_currentApproval.approvalValue)
        {
            case -1:
                dialogueLines = _approvalDialogueLines.dislikeLines;
                break;
            case 0:
                dialogueLines = _approvalDialogueLines.neutralLines;
                break;
            case 1:
                dialogueLines = _approvalDialogueLines.likedLines;
                break;
            default:
                Debug.Log("APPROVAL IS BROKEN...I REPEAT APPROVAL IS BROKEN!!");
                break;
        }
    }
    public void Input()
    {
        if (currentIndex < dialogueLines.Length - 2 && !(currentIndex == questionIndex - 2 || currentIndex == questionIndex - 1))
        {
            _input.text = "Next";
            if (_noButton.activeSelf == true)
            {
                _noButton.SetActive(false);
            }
            currentIndex++;
        }
        else if (currentIndex < dialogueLines.Length - 2 && currentIndex == questionIndex - 2)
        {

            _input.text = "Yes";
            _noButton.SetActive(true);
            currentIndex++;
        }
        else if (currentIndex < dialogueLines.Length - 2 && currentIndex == questionIndex-1)
        {
            _input.text = "Next";
            if (_noButton.activeSelf == true)
            {
                _noButton.SetActive(false);
            }
            if (_currentApproval != null)
            {
                if (_currentApproval.approvalValue < 1)
                {
                    _currentApproval.approvalValue++;
                }
            }
            currentIndex++;
            
        }
        else if (currentIndex < dialogueLines.Length - 1)
        {

            currentIndex++;
            if (_noButton.activeSelf == true)
            {
                _noButton.SetActive(false);
            }
            _input.text = "Bye!";
        }
        else
        {
            currentIndex = 0;
            _input.text = "Next";
            OnDeActive();
        }
        
        ChangeApproval();
        _dialogueText.text = dialogueLines[currentIndex];
    }
    public void Skip()
    {
        if (_currentApproval != null)
        {
            if (_currentApproval.approvalValue > -1)
            {
                _currentApproval.approvalValue--;
            }
        }
        currentIndex = dialogueLines.Length - 1;
        _input.text = "Bye!";
        if (_noButton.activeSelf == true)
        {
            _noButton.SetActive(false);
        }
        ChangeApproval();
        _dialogueText.text = dialogueLines[currentIndex];
    }

}

[System.Serializable]
public struct ApprovalDialogueLines
{
    public string[] dislikeLines;
    public string[] neutralLines;
    public string[] likedLines;
}
