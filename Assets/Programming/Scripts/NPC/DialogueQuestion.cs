using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueQuestion : MonoBehaviour, IInteractable
{
    [SerializeField] string[] _lines;
    [SerializeField] string _name;
    [SerializeField] Sprite _face;
    [SerializeField] int _index;

    public void Interaction()
    {
        DialogueManager.instance.OnActive(_lines, _name, _face, _index);
    }
}
