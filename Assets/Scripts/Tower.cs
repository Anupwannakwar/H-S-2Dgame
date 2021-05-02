using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Rigidbody2D trb;
    private BoxCollider2D colli;
    public Healthbar healthbar;
    public GameObject GOpanel;
    public GameObject GIpanel;

    public int Tower_hp;
    private void Start()
    {
        healthbar.setmaxhealth(Tower_hp);
    }

    // Update is called once per frame
    void Update()
    {
        if(Tower_hp<=0)// if tower hp reaches 0 then healthpanel will be disabled and gameover panel will be enabled.(note that we are using Game over panel instead of game over scene).
        {
            GIpanel.SetActive(false);
            GOpanel.SetActive(true);
        }
    }

    //taking damage
    public void DamageTaken(int damage)
    {
        Tower_hp -= damage;
        healthbar.sethealth(Tower_hp);
    }
}
