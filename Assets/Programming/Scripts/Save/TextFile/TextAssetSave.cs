using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class TextAssetSave : MonoBehaviour
{
    public void CreateText(Text fileName)
    {
        //path
        string path = Application.dataPath + $"/{fileName.text}.txt";
        Debug.Log(path);    
        //create a file if file doesnt exist
        if (!File.Exists(path))
        {
            File.WriteAllText(path,"New");
        }
        //content for the file
        //string content = $"Log Date/Time: {System.DateTime.Now}";
        string content = "";
        //put content into file
        //Replace
        File.WriteAllText(path, content);
       //ADD File.AppendAllText(path,content);
    }
}
