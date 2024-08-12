using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTriggerDoor : MonoBehaviour
{
    [SerializeField] Animator _anim;
    bool _isActive = false;
    bool _canInteract = false;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("Open", _isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _canInteract = false;
        }
    }
    private void Update()
    {
        if (_canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("GetKeyDown");
                _isActive = !_isActive;
                _anim.SetBool("Open", _isActive);
            }
        }
        
    }
}
