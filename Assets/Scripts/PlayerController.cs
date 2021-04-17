using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private SpriteRenderer sprite;
    float horizontal;
    public float playerSpeed;
    public float JumpForce;
    public bool OnGround = true;
    public LayerMask whatIsGround;
    public float groundCheckRange;
    public GameObject GroundCheckPos;
    public bool Sliding = false;

    private Animator anim;

    //for attack
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;

    public CapsuleCollider2D PlayerCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
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
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("IsAttacking", true);
                Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToHit.Length; i++)
                {
                    Debug.Log("Damage enemy");
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
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        Move();
        if(horizontal < 0 && isFacingRight)
        {
            flip();
        }
        else if(horizontal > 0 && !isFacingRight)
        {
            flip();
        }
        if (Mathf.Abs(horizontal * playerSpeed) > 0 && OnGround)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
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
    private void flip()
    {
        if(isFacingRight)
        {
            sprite.flipX = true;
            isFacingRight = !isFacingRight;
        }
        else
        {
            sprite.flipX = false;
            isFacingRight = !isFacingRight;
        }
    }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckPos.transform.position, groundCheckRange);
        Gizmos.DrawWireSphere(attackPos.transform.position, attackRange);
    }
}
