using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed; 

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        FindDirection(); 
    }

    
    void Update()
    {
        Move();
    }
    private void Move()
    {


        Vector2 temp = rigid.velocity;
        temp.x = speed;
        rigid.velocity = temp;
    }

    private void FindDirection()
    {
        if(speed < 0)
        {
            sprite.flipX = true;
        }
        else if(speed > 0)
        {
            sprite.flipX = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.CompareTag("Player"))
        {
            speed = -speed;
            FindDirection();
        }
    }
}
