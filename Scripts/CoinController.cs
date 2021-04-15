using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float speed;
    public float coinSpeed;
    public enum Coin
    {
        FlyCoin,
        DestroyCoin
    }
    public Coin coin;
    private bool isFlying;
    private GameObject hudCoin;

    private void Start()
    {
        isFlying = false;
        hudCoin = GameObject.Find("CoinImg");
    }


    private void Update()
    {
        Rotate();

        if(isFlying)
        {
            transform.position = Vector2.Lerp(transform.position, hudCoin.transform.position, coinSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(coin== Coin.DestroyCoin)
            {
                Destroy(gameObject);

            } 
            else if(coin == Coin.FlyCoin)
            {
                isFlying = true;
            }
        }
    }
    private void Rotate()
    {
        transform.Rotate(new Vector3(0,speed,0));
    }
}
