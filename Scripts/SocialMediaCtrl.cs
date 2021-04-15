using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaCtrl : MonoBehaviour
{
    public string instagram, rating;
    public GameObject socialPanel;

    private bool isOpen;

    private void Start()
    {
        isOpen = false;
    }

    public void instagramPage()
    {
        Application.OpenURL(instagram);
    }
    public void ratingPage()
    {
        Application.OpenURL(rating);
    }
    public void OpenPanel()
    {
        if(isOpen)
        {
            socialPanel.SetActive(false);
            isOpen = false;
        }
        else
        {
            socialPanel.SetActive(true);
            isOpen = true;
        }
        
    }
}
