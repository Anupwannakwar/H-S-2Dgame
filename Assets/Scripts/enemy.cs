using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Rigidbody2D zrb;
    private Collider2D EnemyCollider;
    private SpriteRenderer sprite;

    [Header("Normal Paramters")]
    public int Enemy_Hp = 100;
    public float Enemyspeed;
    public int IncScore;
    private Transform Target; 
    private PlayerController pc;

    [Header("Detections")]
    public LayerMask WhatisPlayer;
    public LayerMask WhatisTower;
    public float StoppingDistance;
    public float PlayerCheckRadius;
    public bool enPlayer;

    private Animator anim;

    [Header("For Attacking")]
    public int Damage;
    public Transform attackPos;
    public float AttackRadius;
    public bool isAttackingTower;
    private float timeBtwAttack;
    private float timeBtwAttack1;
    public float startTimeBtwAttack;
    private ScoreManager sm;
    

    // Start is called before the first frame update
    void Start()
    {
        zrb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        EnemyCollider = GetComponent<CapsuleCollider2D>();

        Target = GameObject.FindGameObjectWithTag("Tower").GetComponent<Transform>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<ScoreManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Target.position) > StoppingDistance && !enPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Enemyspeed * Time.deltaTime);
        }

        //Attacking Tower
        else if (Vector2.Distance(attackPos.position, Target.position) < StoppingDistance)
        {
            if(timeBtwAttack1 <= 0)
            {
                anim.SetBool("IsAttacking", true);
                isAttackingTower = true;
                Collider2D[] TowerToHit = Physics2D.OverlapCircleAll(transform.position, AttackRadius, WhatisTower);
                for (int i = 0; i < TowerToHit.Length; i++)
                {
                    Debug.Log("Damage Tower");
                    TowerToHit[i].GetComponent<Tower>().DamageTaken(Damage);
                }
                timeBtwAttack1 = startTimeBtwAttack;
            }
            timeBtwAttack1 -= Time.deltaTime;
        }
        else if(!enPlayer)
        {
            anim.SetBool("IsAttacking", false);
            isAttackingTower = false;
            
        }
        

        //Attacking player
        bool PlayerToHit = Physics2D.OverlapCircle(attackPos.position, AttackRadius, WhatisPlayer);
        if (!isAttackingTower && timeBtwAttack <= 0)
        {
            if (PlayerToHit )
            {
                anim.SetBool("IsAttacking", true);
                pc.Damagetaken(Damage);
                Debug.Log("Damage player");
                timeBtwAttack = startTimeBtwAttack;
                enPlayer = true;
            }
            if (!PlayerToHit)
            {
                anim.SetBool("IsAttacking", false);
                isAttackingTower = false;
                enPlayer = false;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        //Taking damage
        if (Enemy_Hp<=0)
        {
            anim.SetBool("IsDead", true);
            Destroy(this.EnemyCollider);
            Destroy(this.zrb);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPos.position, AttackRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, PlayerCheckRadius);
    }
    public void Damagetaken(int Damage)
    {
        Enemy_Hp -= Damage;
    }
    public void Destroyenemy()
    {
        sm.updateScore(IncScore);
        Destroy(this.gameObject);
    }
}
