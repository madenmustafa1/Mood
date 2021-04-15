using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUIController : MonoBehaviour
{
    
     PlayerController player;
    
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

   
    public void MoveLeftMobile()
    {
        player.MoveLeftMobile();
    }
    public void MoveRightMobile()
    {
        player.MoveRightMobile();
    }
    public void StopPlayerMobile()
    {
        player.StopPlayerMobile();
    }
    public void JumpMobile()
    {
        player.JumpMobile();
    }
    public void FireMobile()
    {
        player.FireMobile();
    }
}
