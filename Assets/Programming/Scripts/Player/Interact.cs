using System.Collections;
using UnityEngine;

//CONSIDER THE FOLLOWING 
//namespace
//component menu
//restrictions
namespace Player
{
    public class Interact : MonoBehaviour
    {
        public GUIStyle crossHair;
        public LayerMask interactionLayer;
        public bool showToolTip = false;
        public string action, button, instruction;


        private void Update()
        {

            //create a ray (a Ray is ?? a beam, line that comes into contact with colliders)
            Ray interactRay;
            //this ray shoots forward from the center of the camera 
            interactRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //create hit info (this holds the info for the stuff we interact with)
            RaycastHit hitInfo;
            //if this physics ray that gets cast in a direction hits a objuct withing our distance and or layer
            if (Physics.Raycast(interactRay, out hitInfo, 10, interactionLayer))
            {
                Debug.DrawRay(interactRay.origin, transform.forward * 10, Color.green);

                showToolTip = true;
                if (KeyBindManager.keys.Count <= 0)
                {
                    //if our interaction button or key is pressed
                    if (Input.GetKey(KeyCode.E))
                    {
                        Debug.DrawRay(interactRay.origin, transform.forward * 10, Color.red);
                        #region GROSS NO!!
                        //check what we hit and do ther thing 
                        //if (hitInfo.collider.CompareTag("Door"))
                        //{
                        //    //do thing
                        //    if (hitInfo.collider.GetComponent<RayDoor>())
                        //    {
                        //        hitInfo.collider.GetComponent<RayDoor>().Interaction();
                        //    }
                        //}
                        //if (hitInfo.collider.CompareTag("NPC"))
                        //{
                        //    if (hitInfo.collider.GetComponent<IMGUIDLG>())
                        //    {
                        //        hitInfo.collider.GetComponent<IMGUIDLG>().Interaction();
                        //    }
                        //}
                        //if (hitInfo.collider.CompareTag("Chest"))
                        //{

                        //}
                        //if (hitInfo.collider.CompareTag("Item"))
                        //{

                        //}
                        //if (hitInfo.collider.CompareTag("Bed"))
                        //{

                        //}
                        //if (hitInfo.collider.CompareTag("Campfire"))
                        //{

                        //}
                        //if (hitInfo.collider.CompareTag("CraftingStation"))
                        //{

                        //}
                        #endregion
                        #region YAS
                        if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interact))
                        {
                            //do thing
                            interact.Interaction();
                        }
                        #endregion
                    }
                }
                else
                {
                    //if our interaction button or key is pressed
                    if (Input.GetKey(KeyBindManager.keys["Interact"]))
                    {
                        Debug.DrawRay(interactRay.origin, transform.forward * 10, Color.red);
                  
                        #region YAS
                        if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interact))
                        {
                            //do thing
                            interact.Interaction();
                        }
                        #endregion
                    }
                }
               
            }
            else
            {
                showToolTip = false; ;
            }
        }
        void OnGUI()
        {
            //for (int x = 0; x < 16; x++)
            //{
            //    for (int y = 0; y < 9; y++)
            //    {
            //        GUI.Box(UIPos(x, y, 1, 1), "");
            //        GUI.Label(UIPos(x, y, 1, 1), x + ":" + y);
            //    }
            //}

            GUI.Box(UIPos(7.75f, 4.25f, 0.5f, 0.5f), "", crossHair);
            if (showToolTip)
            {
                GUI.Box(UIPos(6.5f, 3.75f, 3f, 0.5f), $"{action} {button} {instruction}");
            }
        }
        Rect UIPos(float startX, float startY, float sizeX, float sizeY)
        {
            return new Rect(startX * (Screen.width / 16), startY * (Screen.height / 9), sizeX * (Screen.width / 16), sizeY * (Screen.height / 9));
        }
    }
}

