using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapı : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) ;
        {
            GameController.instance.LevelComplete();
        }
    }
}
