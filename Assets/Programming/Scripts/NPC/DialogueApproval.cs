using UnityEngine;

public class DialogueApproval : MonoBehaviour, IInteractable
{
    [SerializeField] ApprovalDialogueLines _approvalDialogueLines;
    [SerializeField] string _name;
    [SerializeField] Sprite _face;
    [SerializeField] int _index;
    public int approvalValue;
    public void Interaction()
    {
        DialogueManager.instance.OnActive(_approvalDialogueLines, _name,_face,_index, this);
    }
}


//Scriptable for the approval value of character

//