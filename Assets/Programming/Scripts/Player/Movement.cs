using UnityEngine;

//this is our family name for the scripts
namespace Player
{
    [AddComponentMenu("GameDev/Player/First Person Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        #region Variables 
        //the direction we are moving
        [SerializeField] Vector3 _moveDirection;
        //the reference to the CharacterController
        [SerializeField] CharacterController _characterController;
        //walk, crouch, sprint, jump, gravity
        [SerializeField] float _movementSpeed, _walk = 5, _run = 10, _crouch = 2.5f, _jump = 8, _gravity = 20;

        Vector2 newInput;
        #endregion
        #region Functions
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }
        private void Update()
        {
            if (GameManager.instance.gameState == GameManager.GameStates.Playing)
            {
                //speed change
                //_movementSpeed, walk ,run ,crouch 
                //Left Shift and Left Control
                #region Option 1
                //if (Input.GetKey(KeyCode.LeftShift))
                //{
                //    _movementSpeed = _run;
                //}
                //else if (Input.GetKey(KeyCode.LeftControl))
                //{
                //    _movementSpeed = _crouch;
                //}
                //else 
                //{ 
                //    _movementSpeed = _walk; 
                //}
                #endregion
                
                //moving the character
                //if our reference to the character controller has a value aka we ected it yay!!! woop 
                if (_characterController != null)
                {
                    //check of we are on the ground so we can move coz thats how people work 
                    if (_characterController.isGrounded)
                    {
                        if(KeyBindManager.keys.Count <= 0)
                        {
                            //what is our direction? set the move direction based off inputs
                            #region Option 2
                            _movementSpeed = Input.GetKey(KeyCode.LeftShift) ? _run : Input.GetKey(KeyCode.LeftControl) ? _crouch : _walk;
                            #endregion
                            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                        }
                        else
                        {
                            newInput.x = Input.GetKey(KeyBindManager.keys["Left"]) ? -1 : newInput.x = Input.GetKey(KeyBindManager.keys["Right"]) ? 1 : 0;
                            newInput.y = Input.GetKey(KeyBindManager.keys["Forward"]) ? 1 : newInput.y = Input.GetKey(KeyBindManager.keys["Backward"]) ? -1 : 0;
                            _movementSpeed = Input.GetKey(KeyBindManager.keys["Sprint"])? _run: _movementSpeed = Input.GetKey(KeyBindManager.keys["Crouch"])? _crouch: _walk;
                            _moveDirection = new Vector3(newInput.x, 0, newInput.y);
                        }
                        //make sure that the direction forward is according to the players forward and not the world north
                        _moveDirection = transform.TransformDirection(_moveDirection);
                        //apply speed to the movement direction
                        _moveDirection *= _movementSpeed;

                        //if we jump
                        if (KeyBindManager.keys.Count <= 0)
                        {
                            if (Input.GetButton("Jump"))
                            {
                                //move up
                                _moveDirection.y = _jump;
                            }
                        }
                        else
                        {
                            if (Input.GetKey(KeyBindManager.keys["Jump"]))
                            {
                                //move up
                                _moveDirection.y = _jump;
                            }
                        }
                    }
                    //add gravity to direction
                    _moveDirection.y -= _gravity* Time.deltaTime;
                    //apply movement
                    _characterController.Move(_moveDirection * Time.deltaTime);
                }
                else
                {
                    Debug.LogWarning("!!!MISSING CHARACTER CONTROLLER CONNECTION FOR THE PLAYER!!!!!");
                }
            }
        }
        #endregion
    }
}
