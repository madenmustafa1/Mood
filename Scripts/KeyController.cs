using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public int keyNumber;
    private void OnTriggerEnter2D(Collider2D other)
    {
         if(other.gameObject.CompareTag("Player"))
        {
            GameController.instance.KeyCount(keyNumber);
            EffectController.instance.ShowKeyEffect(keyNumber);
            AudioController.instance.KeySound(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
