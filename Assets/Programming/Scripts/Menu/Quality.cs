using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quality : MonoBehaviour
{
    public void ChangeQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
