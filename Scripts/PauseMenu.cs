using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ShowPauseMenu()
    {
        GameController.instance.ShowPauseMenu();
    }
    public void HidePauseMenu()
    {
        GameController.instance.HidePauseMenu();
    }
    public void MusicOnOff()
    {
        AudioController.instance.MusicOnOff();
    }
}
