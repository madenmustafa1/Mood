using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player hareketleri zıplama, sağa sola dönme
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Tooltip("Only positive values for speed")]
    public float speed;
    public float jumpForce;  
    public float feetRadius; 
    public bool isJumping;
    public float width, height;
    public float jumpDelay;
    public float nextFire = 0.5f;
    public bool effect;
    public bool leftClicked, rightClicked;
    public bool isGrounded;
    public GameObject deathTrigger;

    public GameObject rightbullet,leftbullet;
    public LayerMask myLayer;
    public Transform feet;
    public Transform leftBulletPos;
    public Transform rightBulletPos;
    public Rigidbody2D rigid;

    private Animator anim;   
    private SpriteRenderer sprite;
    private bool doubleJump;
    private float timer;    
    private bool canFire;
    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    } 

    private void FixedUpdate()
    {
        timer += Time.deltaTime;



        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(width, height),360f,myLayer);
        float h = Input.GetAxisRaw("Horizontal");
       
        if(h!=0)
        {
            MovePlayer(h);
        }
        else
        {
            StopPlayer();
        } 
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if(Input.GetButtonDown("Fire1")&& timer>nextFire)
        {
            FireBullet();
        }


        if(leftClicked)
        {
            MovePlayer(-1f);
        }
        if(rightClicked)
        {
            MovePlayer(1f);
        }

        PlayerFall();
    }
    private void FireBullet()
    {
      if(canFire)
        {
            timer = 0f;
            if (!sprite.flipX)
            {
                Instantiate(rightbullet, rightBulletPos.position, Quaternion.identity);

            }
            if (sprite.flipX)
            {
                Instantiate(leftbullet, leftBulletPos.position, Quaternion.identity);
            }
            AudioController.instance.FireSound(gameObject.transform.position);
        }

    }
    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireSphere(feet.position, feetRadius);


        Gizmos.DrawWireCube(feet.position, new Vector3(width, height,0f));
    }
    private void MovePlayer(float h)
    {

        rigid.velocity = new Vector2(h * speed, rigid.velocity.y);
        


        if ( h<0)
        {
            sprite.flipX = true;
        }
        else if ( h>0)
        {
            sprite.flipX = false;
        }
        if(!isJumping)
        {
            anim.SetInteger("Status", 1);
        }

    }
    private void PlayerFall()
    {
        if(rigid.velocity.y<0)
        {
            anim.SetInteger("Status", 3);
        }
    }
    private void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
            rigid.AddForce(new Vector2(0f, jumpForce));


            if (isJumping)
            {
                anim.SetInteger("Status", 2);
                AudioController.instance.JumpSound(gameObject.transform.position);
                Invoke("DoubleJump", jumpDelay);
            }
        }
            if (doubleJump && !isGrounded)
            {

                rigid.velocity = Vector2.zero;

                rigid.AddForce(new Vector2(0f, jumpForce));
                anim.SetInteger("Status", 2);
            AudioController.instance.JumpSound(gameObject.transform.position);
            //tekrar zıplamaması için false yapmak gerekiyor.
            doubleJump = false;
            }
         
    }
    private void DoubleJump()
    {
        doubleJump = true;
    }
    private void  StopPlayer()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);

       if(!isJumping)
        {
            anim.SetInteger("Status", 0);
        }
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isJumping = false;
        if(other.gameObject.CompareTag("Enemy"))
        {
            GameController.instance.PlayerHit(gameObject);
            AudioController.instance.PlayerDieSound(gameObject.transform.position);
        }
        if (other.gameObject.CompareTag("RewardCoin"))
        {
            GameController.instance.CoinCount();
            EffectController.instance.ShowCoinEffect(other.transform.position);
            Destroy(other.gameObject);
            GameController.instance.ScoreCount(20);
            AudioController.instance.CoinSound(other.transform.position);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Coin":
                if(effect)
                {
                    EffectController.instance.ShowCoinEffect(other.gameObject.transform.position);
                    GameController.instance.CoinCount();
                    GameController.instance.ScoreCount(5);
                    AudioController.instance.CoinSound(gameObject.transform.position);
                }
                break;
            case "PowerUp":
                if (effect)
                {
                    canFire = true;
                    EffectController.instance.ShowPowerUpEffect(other.gameObject.transform.position);
                    Destroy(other.gameObject);
                    AudioController.instance.KeySound(other.transform.position);
                }
                break;
            case "Water":
                    deathTrigger.SetActive(false);
                    EffectController.instance.ShowWaterEffect(gameObject.transform.position);
                    GameController.instance.PlayerDied(gameObject);
                    AudioController.instance.WaterSound(gameObject.transform.position);
                    AudioController.instance.PlayerDieSound(gameObject.transform.position);
                break;
            case "BossKey":
                GameController.instance.DisableWall();

            break;

        }
    }
    public void MoveLeftMobile()
    {
        leftClicked = true;
    }
    public void MoveRightMobile()
    {
        rightClicked = true;
    }
    public void StopPlayerMobile()
    {
        rightClicked = false;
        leftClicked = false;
    }         
    public void JumpMobile()
    {
        Jump();
    }
    public void FireMobile()
    {
        FireBullet();
    }

   

}
