using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform left, right;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private bool turn;
    private float currentSpeed;
    private Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        FindDirection();
        turn = true;
        
    }


    void Update()
    {
        MoveEnemy();
        TurnEnemy(); 
    }

    private void MoveEnemy()
    {
        rigid.velocity = new Vector2(speed, 0f); 
    }
    private void FindDirection()
    {
        if (speed < 0)
        {
            sprite.flipX = true;
        }
        else if (speed > 0)
        {
            sprite.flipX = false;
        }
    }
    private void TurnEnemy()
    {
        if(!sprite.flipX && transform.position.x >= right.position.x)
        {
            if(turn)
            {
                turn = false;
                currentSpeed = speed;
                speed = 0;
                StartCoroutine("TurnLeft", currentSpeed);
            }

        }
        else if(sprite.flipX && transform.position.x <= left.position.x)
        {
            
            if (turn)
            {
                turn = false;
                currentSpeed = speed;
                speed = 0;
                StartCoroutine("TurnRight", currentSpeed);
            }

        }
    }

    IEnumerator TurnLeft(float currentSpeed)
    {
        anim.SetBool("Idle", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Idle", false);
        sprite.flipX = true;
        speed = -currentSpeed;
        turn = true;
    }
    IEnumerator TurnRight(float currentSpeed)
    {
        anim.SetBool("Idle", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Idle", false);
        sprite.flipX = false;
        speed = -currentSpeed;
        turn = true;
    }
}
