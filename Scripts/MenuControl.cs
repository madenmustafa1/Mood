using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
   public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//mevcut leveli restart edecek.
    }
}
