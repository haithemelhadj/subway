using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    public Vector3[] spawnPoints;
    public float spawnTime;
    private float cooldown;
    public GameObject[] obstacle;

    public GameObject coin;
    public float coinSpawnTime;
    private float coinCooldown;
    public bool canSpawnCoin;
 


    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            spawn();
            cooldown = spawnTime;
        }
        coinCooldown -= Time.deltaTime;
        if (coinCooldown <= 0)
        {
            spawnCoins();
            coinCooldown = coinSpawnTime;
        }
    }

    private int coinPos;

    void spawn()
    {
        int i= Random.Range(0, spawnPoints.Length);
        coinPos = i;
        while(coinPos == i)
        {
            coinPos = Random.Range(0, spawnPoints.Length);
        }
        int j= Random.Range(0, obstacle.Length);
        if (j == 1)
        {
            Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
            Instantiate(obstacle[j], spawnPoints[i], rotation);
        }
        else
        {
            Instantiate(obstacle[j], spawnPoints[i], Quaternion.identity);
            
        }

    }
    private void spawnCoins()
    {
        Instantiate(coin, spawnPoints[coinPos], Quaternion.identity);
        
    }
}
