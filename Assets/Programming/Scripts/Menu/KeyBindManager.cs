using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyBindManager : MonoBehaviour
{
    [Serializable]
    public struct ActionData
    {
        public string actionName;
        public Text keyDisplay;
        public string defaultKey;
    }
    [SerializeField] ActionData[] actionSetUp;
    public static Dictionary<string,KeyCode> keys = new Dictionary<string,KeyCode>();
    public GameObject currentKey;
    public Color32 selectedColour = new Color32(239, 116, 36, 255);
    public Color32 changedColour = new Color32(39, 171, 249, 255);

    private void Start()
    {
        for (int i = 0; i < actionSetUp.Length; i++)
        {
            keys.Add(actionSetUp[i].actionName, (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(actionSetUp[i].actionName,actionSetUp[i].defaultKey)));

            actionSetUp[i].keyDisplay.text = keys[actionSetUp[i].actionName].ToString();
        }
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
    public void ChangeKey(GameObject clickedKey)
    {
        currentKey = clickedKey;
        if(clickedKey != null)
        {
            currentKey.GetComponent<Image>().color = selectedColour;
        }
    }
    private void OnGUI()
    {
        string newKeyCode = "";
        Event changeKeyEvent = Event.current; 
        if (currentKey != null)
        {
            if(changeKeyEvent.isKey)
            {
                newKeyCode = changeKeyEvent.keyCode.ToString();
            }
            if(newKeyCode != "")
            {
                keys[currentKey.name] = (KeyCode)Enum.Parse(typeof(KeyCode), newKeyCode);
                currentKey.GetComponentInChildren<Text>().text = newKeyCode;
                currentKey.GetComponent<Image>().color = changedColour;
                currentKey = null;
            }
        }
    }
}
