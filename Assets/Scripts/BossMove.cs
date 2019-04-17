using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    public GameObject boss;
    public NavMeshAgent agent;
    NavMeshPath NavPath;
    public int pauseTime = 3;
    public bool Coroutine, validPath;
    public Vector3 target;
    Vector3 lastTargetLocation;

    void Start()
    {
        boss = this.gameObject;
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;

        agent = GetComponent<NavMeshAgent>();
        NavPath = new NavMeshPath();
    }

    void Update()
    {
        if (!Coroutine)
            StartCoroutine(NewPath());
    }

    public Vector3 RandomMove()
    {
        float x = Random.Range(-8, 8);
        float z = Random.Range(-8, 8);

        Vector3 position = new Vector3(x, 0, z);
        return position;
    }

    IEnumerator NewPath()
    {
        Coroutine = true;
        yield return new WaitForSeconds(pauseTime);
        GetPath();

        validPath = agent.CalculatePath(target, NavPath);
        if (!validPath)
            Debug.Log("Invalid path");

        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            NewPath();
            validPath = agent.CalculatePath(target, NavPath);
        }

        Coroutine = false;
    }

    void GetPath()
    {
        target = RandomMove();
        agent.SetDestination(target);
    }
}
