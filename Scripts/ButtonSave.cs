using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSave : MonoBehaviour
{
    public void SaveData()
    {
        DataCtrl.instance.SaveData();
    }
}
