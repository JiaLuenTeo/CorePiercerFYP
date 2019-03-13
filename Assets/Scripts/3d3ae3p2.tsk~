using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum States
    {
        IDLE,
        SPAWNING,
        ROUNDONGOING,
        ROUNDENDED,
        ROUNDINTERMISSION
    }

    public GameObject enemyGo;
    public float intermissionTime = 5.0f;
    public List<int> enemyPerRound = new List<int>();
    public int currentRound;
    public int enemyAlive;
    public float waitTime = .5f;

    public States curState;
    public States preState;

    public float oriTime;
    float time;
    int enemyCounter;
    Vector3 spawnLocation;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (curState == States.IDLE || curState == States.SPAWNING)
        {
            startSpawning();
        }
        else if (curState == States.ROUNDONGOING)
        {
            checkForEnemies();
        }
        else if (curState == States.ROUNDINTERMISSION)
        {
            waitForNextRound();
        }
        else if (curState == States.ROUNDENDED)
        {
            checkVictoryCondition();
        }

    }

    void startSpawning()
    {
        curState = States.SPAWNING;

        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");

        time += Time.deltaTime;

        if (time > waitTime)
        {
            int spawn = Random.Range(1, spawnPoint.Length + 1);

            if (spawn == 1)
            {
                spawnLocation = spawnPoint[0].transform.position;
            }
            else if (spawn == 2)
            {
                spawnLocation = spawnPoint[1].transform.position;
            }
            else if (spawn == 3)
            {
                spawnLocation = spawnPoint[2].transform.position;
            }
            else if (spawn == 4)
            {
                spawnLocation = spawnPoint[3].transform.position;
            }

            GameObject go = Instantiate(enemyGo, spawnLocation, Quaternion.identity);
           // go.GetComponent<NavMeshTarget>().target = GameObject.FindGameObjectWithTag("Player").transform;
            time = 0;
            enemyCounter++;
        }

        if (enemyCounter == enemyPerRound[currentRound]) curState = States.ROUNDONGOING;
    }

    void checkForEnemies()
    {
        GameObject[] enemySpawned = GameObject.FindGameObjectsWithTag("Enemy");
        enemyAlive = enemySpawned.Length;
        if (enemySpawned.Length <= 0)
        {
            currentRound++;
            oriTime = intermissionTime;
           // GameManagerTPS.Instance.DifficultyChange();
            waitTime -= 0.1f;
            curState = States.ROUNDENDED;
        }

    }

    void checkVictoryCondition()
    {
        if (currentRound == enemyPerRound.Count)
        {
            //GameManagerTPS.Instance.WinState();
        }
        else
        {
            curState = States.ROUNDINTERMISSION;
        }
    }

    void waitForNextRound()
    {
        if (intermissionTime <= 0)
        {
            enemyCounter = 0;
            intermissionTime = oriTime;
            //GameManagerTPS.Instance.clearAllAmount++;
            curState = States.IDLE;
        }
        else
        {
            intermissionTime -= Time.deltaTime;
        }
    }
}

