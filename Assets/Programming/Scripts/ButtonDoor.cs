using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDoor : MonoBehaviour
{
    //Door Animation
    [SerializeField] Animator _doorAnim;
    //Button Animation
    [SerializeField] Animator _buttonAnim;
    //Timer
    public float timer = 0;
    //Max Amount of time
    [SerializeField] float _maxTime = 3;
    //Can we press the button?
    [SerializeField] bool _canPressButton = false;
    //Display of Timer
    public Text timerDisplayText;

    private void Start()
    {
        _doorAnim.SetBool("Open", false);
        _canPressButton = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _canPressButton = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _canPressButton = false;
        }
    }

    private void Update()
    {
        if(_canPressButton)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Press button
                _buttonAnim.SetTrigger("Pressed");
                //Set door to open
                _doorAnim.SetBool("Open",true);
                //timer gets set to max time;
                timer = _maxTime;
            }
        }
        //if the timer is greater than 0
        if (timer > 0)
        {
            //count down
            timer -= Time.deltaTime;
            //update UI
            timerDisplayText.text = string.Format("{0:0.00}",timer);
            
        }

        //if timer is less than or equal to 0 
        if (timer <= 0 && timerDisplayText.text != "")
        {
            //close door
            _doorAnim.SetBool("Open", false);
            //hide ui 
            timerDisplayText.text = "";
        }

    }
}
