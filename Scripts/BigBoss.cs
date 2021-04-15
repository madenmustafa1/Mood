using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BigBoss : MonoBehaviour
{
    public float speed;
    public float jumpAt;
    public GameObject bullet;
    public GameObject bulletPos;
    public float nextFire;
    public float enemyHealth;
    public Slider healthSlider;


    private bool canFire, isJumping;
    private Rigidbody2D rigid;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        canFire = false;
        Invoke("CanFire", Random.Range(1, nextFire));
    }

   
    void Update()
    {
        if(canFire)
        {
            EnemyFire();
            canFire = false;
        }
        if(enemyHealth < jumpAt && !isJumping)
        {
            InvokeRepeating("JumpEnemy", 0, 2);
            isJumping = true;
        }
    }
    private void JumpEnemy()
    {
        rigid.AddForce(new Vector2(0f, speed));
    }
    private void CanFire()
    {
        canFire = true;
    }
    private void EnemyFire()
    {
        Instantiate(bullet, bulletPos.transform.position, Quaternion.identity);
        Invoke("CanFire", Random.Range(1, nextFire));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            if(enemyHealth == 0)
            {
                GameController.instance.BulletHit(gameObject);
            }
            if(enemyHealth > 0 )
            {
                enemyHealth--;
                healthSlider.value = enemyHealth;
                gameObject.GetComponent<Animation>().Play("DamagedBoss");
            }


        }
    }
}
