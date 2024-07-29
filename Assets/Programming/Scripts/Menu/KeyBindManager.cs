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
}
