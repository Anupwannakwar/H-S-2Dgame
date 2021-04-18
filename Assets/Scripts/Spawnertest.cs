using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnertest : MonoBehaviour
{
    public GameObject Spawn;
    public GameObject Zombie;
    public bool stopspawning = false;
    int[] spawnquantity = new int[3] { 1,2,3 };
    int[] spawntime = new int[10] { 1,2,3,4,5,6,7,8,9,10 };
    int i;
    public float startSpwanTime;
    public float currentSpwanTime;

    void Start()
    {
        currentSpwanTime = startSpwanTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpwanTime <= 0)
        {
            i = Random.Range(0, 10);

            if (spawntime[i] == 4 || spawntime[i] == 8)
            {
                Instantiate(Zombie, Spawn.transform.position, Spawn.transform.rotation);
            }
            currentSpwanTime = startSpwanTime;
        }
        else
        {
            currentSpwanTime -= Time.deltaTime;
        }
    }
}
