using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    
    public static EffectController instance;
    public Effect effect;
    public Transform blue, green, yellow;
   
    private void Awake()
    {
        if(instance==null)
        {
            instance = this; 
        }
    }

    public void ShowCoinEffect(Vector3 pos)
    {
        Instantiate(effect.coinEffect, pos, Quaternion.identity);
    }
    public void ShowPowerUpEffect(Vector3 pos)
    {
        Instantiate(effect.powerUpEffect, pos, Quaternion.identity);
    }
    public void ShowDustEffect(Vector3 pos)
    {
        Instantiate(effect.dustEffect, pos, Quaternion.identity);
    }

    public void ShowWaterEffect(Vector3 pos)
    {
        Instantiate(effect.waterEffect, pos, Quaternion.identity);
    }
    public void ShowKeyEffect(int val)
    {
        Vector3 pos = new Vector3(0, 0, 0);
        if (val == 0)
            pos = blue.position;
        else if (val == 1)
            pos = green.position;
        else if (val == 2)
            pos = yellow.position;
        Instantiate(effect.powerUpEffect, pos, Quaternion.identity);
    }

    public void EnemyDie(GameObject enemy)
    {
        Instantiate(effect.enemyExplosion, enemy.transform.position, Quaternion.identity);
    }

}

[System.Serializable]
public class Effect
{
    public GameObject coinEffect;
    public GameObject powerUpEffect;
    public GameObject dustEffect;
    public GameObject waterEffect;
    public GameObject enemyExplosion;
}
