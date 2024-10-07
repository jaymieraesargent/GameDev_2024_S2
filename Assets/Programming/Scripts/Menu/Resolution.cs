using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    public void FullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public UnityEngine.Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    private void Start()
    {
        if(resolutionDropdown != null)
        {
            //getting all resolutions for this computers screen settings
            resolutions = Screen.resolutions;
            //reset and empty the dropdown
            resolutionDropdown.ClearOptions();
            //get ready to make new dropdown list
            List<string> options = new List<string>();
            //get ready to set the current resolution when found
            int currentResolutionIndex = 0;
            //loop through all options the computer has
            for (int i = 0; i < resolutions.Length; i++) 
            {
                //hold formatted option
                string option = $"{resolutions[i].width} x {resolutions[i].height}";
                //add option to list
                options.Add(option);
                //if option matches resolution then set this as current
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }
    public void SetResolution(int resIndex)
    {
        UnityEngine.Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width,res.height, Screen.fullScreen);
    }
}
