using UnityEngine;

namespace Player
{
    [AddComponentMenu("GameDev/Player/First Person Mouse Look")]
    public class MouseLook : MonoBehaviour
    {
        #region Rotational axis
        /*
            We are going to use something known as a state value
            variable, also know as a comma seperated list of identifiers
            also known as the Enumeration type enum. 
         */
        public enum RotationalAxis
        {
            MouseMovementX,
            MouseMovementY
        }
        #endregion
        #region Variables
        //Reference to the enum RotationalAxis so we can use it
        [SerializeField] RotationalAxis _axis = RotationalAxis.MouseMovementX;
        //A way to control the mouse speed/sensitivity
        [SerializeField] float _sensitivity = 15.0f;
       public float Sensitivity
        {
            get { return _sensitivity; }
            set { _sensitivity = value; }
        }
        //Max/Min Rotation range so we done have a head that looks up and ends up doing 360s
        [SerializeField] Vector2 _rotationRangeY = new Vector2(-60.0f, 60.0f);
        //Placeholder Temp value for the yrotation so we can invert it if needed
        float _rotationY;
        bool _invert = false;
        public bool Invert
        {
            get { return _invert; }
            set { _invert = value; }
        }
        #endregion
        #region Functions
        private void Awake()
        {
            if(GetComponent<Rigidbody>())
            {
                //GetComponent<Rigidbody>().freezeRotation = true;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            if(GetComponent<Camera>())
            {
                _axis = RotationalAxis.MouseMovementY;
            }
        }
        private void Update()
        {
            if (GameManager.instance.gameState == GameManager.GameStates.Playing)
            {
                #region Horizontal Mouse Movement
                if (_axis == RotationalAxis.MouseMovementX)
                {
                    transform.Rotate(0, Input.GetAxis("Mouse X") * _sensitivity, 0);
                }
                #endregion
                #region Vertical Mouse Movement
                else
                {
                    //get the rotation direction and speed
                    _rotationY += Input.GetAxis("Mouse Y") * _sensitivity;
                    //clamp
                    _rotationY = Mathf.Clamp(_rotationY, _rotationRangeY.x, _rotationRangeY.y);
                    //apply
                    if (!_invert) // normal
                    {
                        transform.localEulerAngles = new Vector3(-_rotationY, 0, 0);

                    }
                    else // plane
                    {
                        transform.localEulerAngles = new Vector3(_rotationY, 0, 0);
                    }
                }
                #endregion
            }
        }
        #endregion
    }
}

