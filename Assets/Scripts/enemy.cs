using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Rigidbody2D zrb;
    private Collider2D EnemyCollider;
    private SpriteRenderer sprite;
    public Transform Target;
    public LayerMask WhatisPlayer;
    public LayerMask WhatisTower;

    private Animator anim;

    public float Enemyspeed;
    public float PlayerCheckRadius;

    //For Attack
    public float Damage;
    public float AttackRadius;
    public float StoppingDistance;
    //public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        zrb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        EnemyCollider = GetComponent<CapsuleCollider2D>();

        Target = GameObject.FindGameObjectWithTag("Tower").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Target.position) > StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Enemyspeed * Time.deltaTime);
        }

        //Attacking Tower
        else if (Vector2.Distance(transform.position, Target.position) < StoppingDistance)
        {
            anim.SetBool("IsAttacking", true);
            //isAttacking = true;
            Collider2D[] TowerToHit = Physics2D.OverlapCircleAll(transform.position, AttackRadius, WhatisTower);
            for (int i = 0; i < TowerToHit.Length; i++)
            {
                Debug.Log("Damage Tower");
            }
        }
        else
        {
            anim.SetBool("IsAttacking", false);
            //isAttacking = false;
        }

        bool PlayerToHit = Physics2D.OverlapCircle(transform.position, AttackRadius, WhatisPlayer);
 
            if(PlayerToHit)
            {
                anim.SetBool("IsAttacking", true);
                Debug.Log("Damage player");
                //isAttacking = true;
            }
            if(!PlayerToHit)
            {
                anim.SetBool("IsAttacking", false);
            }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, PlayerCheckRadius);
    }
}
