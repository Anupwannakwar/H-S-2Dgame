using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Rigidbody2D trb;
    private BoxCollider2D colli;

    public int Tower_hp;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Tower_hp<=0)
        {
            Debug.Log("gameover");
        }
    }
    public void DamageTaken(int damage)
    {
        Tower_hp -= damage;
    }
}
