using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private SpriteRenderer sprite;
    private float horizontal;
    public GameObject GOpanel;
    public GameObject GIpanel;

    [Header("Normal Paramters")]
    public int Player_HP;
    public float playerSpeed;
    public float JumpForce;
    public bool OnGround = true;
    public bool Sliding = false;
    public CapsuleCollider2D PlayerCollider;
    public Healthbar healthbar;

    [Header("Detections")]
    public float groundCheckRange;
    public LayerMask whatIsGround;
    public GameObject GroundCheckPos;

    private Animator anim;

    [Header("Attacking")]
    public int damage;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float attackRange;
    public Transform attackPos;
    public LayerMask whatIsEnemy;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
        healthbar.setmaxhealth(Player_HP);
    }

    private void Update()
    {
        //checking ground
        OnGround = Physics2D.OverlapCircle(GroundCheckPos.transform.position, groundCheckRange, whatIsGround);

        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            jump();
        }
        
        if(OnGround)
        {
            anim.SetBool("IsJumping", false);
        }
        else
        {
            anim.SetBool("IsJumping", true);
        }

        //Sliding
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Sliding = true;
            StartCoroutine(SlideWait(0.5f));
        }

        //attacking
        if (timeBtwAttack <= 0)//for setting time between attacks(so that player can only attack after specified time interval between first attack and another)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("IsAttacking", true);
                Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToHit.Length; i++)
                {
                    Debug.Log("Damage enemy");
                    enemiesToHit[i].GetComponent<enemy>().Damagetaken(damage);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                anim.SetBool("IsAttacking", false);
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        //taking Damage
        if(Player_HP<=0)
        {
            anim.SetBool("IsDead", true);
        }
    }

    void FixedUpdate()
    {
        //movement
        horizontal = Input.GetAxis("Horizontal");
        Move();
        if(horizontal < 0 && isFacingRight)// if movement is happening and facing right then flip player facing right
        {
            flip();
        }
        else if(horizontal > 0 && !isFacingRight)// if movement is happening and facing left then flip player facing left
        {
            flip();
        }
        if (Mathf.Abs(horizontal * playerSpeed) > 0 && OnGround)//if player is moving and is on ground then perform running animation
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    //move funtion
    private void Move()
    {
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);

        //for sliding
        if (Sliding && rb.velocity.x != 0)
        {
            PlayerCollider.size = new Vector2(2.32f, 2.7f);
            anim.SetBool("IsSliding", true);
        }
        else
        {
            anim.SetBool("IsSliding", false);
        }

    }

    // flipping character when looking another side
    private void flip()
    {
        if(isFacingRight)
        {
            transform.rotation = new Quaternion(0, 180, 0,0);
            isFacingRight = !isFacingRight;
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            isFacingRight = !isFacingRight;
        }
    }

    //jump function
    private void jump()
    {
        rb.AddForce(Vector2.up * JumpForce);
    }

    IEnumerator SlideWait(float secs)
    {
        yield return new WaitForSeconds(secs);
        Sliding = false;
        PlayerCollider.size = new Vector2(2.32f, 4.39f);
        yield break;
    }

    //gizmos for player detection ranges
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckPos.transform.position, groundCheckRange);
        Gizmos.DrawWireSphere(attackPos.transform.position, attackRange);
    }

    //damage taking function as well as display player current HP
    public void Damagetaken(int Damage)
    {
        Player_HP -= Damage;
        healthbar.sethealth(Player_HP);
    }

    // destroy player function
    public void Destroyplayer()
    {
        Destroy(this.gameObject);
        GIpanel.SetActive(false);
        GOpanel.SetActive(true);
    }

    // on trigger for heart detection and increasing player current HP
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Heart"))
        {
            Destroy(other.gameObject);
            Player_HP = Player_HP + 20;
        }
    }
}
