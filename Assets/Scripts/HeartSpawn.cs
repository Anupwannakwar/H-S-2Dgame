using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawn : MonoBehaviour
{
    public GameObject Spawnner;
    public GameObject Heart;
    public bool stopspawning = false;
    int[] hspawntime = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    int j;
    int l;
    public float startSpawnTime;
    public float currentSpawnTime;
    public PlayerController playercon;

    void Start()
    {
        currentSpawnTime = startSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawnTime <= 0 && playercon.Player_HP != 100)
        {
            j = Random.Range(0, 10);
            l = Random.Range(0, 10);

            if (hspawntime[j] == 5|| hspawntime[l] == 5)//heart will only spawn if j and l value is 5 otherwise it will continue checking for j and l value.
            {
                Instantiate(Heart, Spawnner.transform.position, Spawnner.transform.rotation);
            }
            currentSpawnTime = startSpawnTime;
        }
        else
        {
            currentSpawnTime -= Time.deltaTime;
        }
    }
}
