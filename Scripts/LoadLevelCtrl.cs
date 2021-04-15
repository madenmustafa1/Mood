using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevelCtrl : MonoBehaviour
{
    public static LoadLevelCtrl instance = null;
    public GameObject loadingUI;
    public Slider slider;

    AsyncOperation test;

    public void Start()
    {
        if(instance == null)
           instance = this;
        
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadNextLevel(levelName));
    }

    IEnumerator LoadNextLevel(string levelName)
    {
        loadingUI.SetActive(true);
        test = SceneManager.LoadSceneAsync(levelName);
        test.allowSceneActivation = false;

        while (test.isDone == false)
        {
            slider.value = test.progress; 
            if(test.progress == 0.9)
            {
                slider.value = 1f;
                test.allowSceneActivation = true;
            }
            yield return null;
        }
        
    }

}
