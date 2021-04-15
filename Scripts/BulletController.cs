using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector2 speed;

    private Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        //mood'un dosyası
    }

    
    void Update()
    {
        rigid.velocity = speed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            GameController.instance.BulletHit(other.gameObject);
            Destroy(gameObject);
        }
        else if(!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
