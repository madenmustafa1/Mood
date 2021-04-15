using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour
{
    public Transform pos;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.CompareTag("ArkaPlan"))
        {
            EffectController.instance.ShowDustEffect(pos.position);
        }
    }
}
