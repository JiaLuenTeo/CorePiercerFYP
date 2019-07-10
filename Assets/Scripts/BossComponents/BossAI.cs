using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossAI : MonoBehaviour
{

    private static BossAI mInstance = null;
    public static BossAI Instance;

    public enum BossCurrentState
    {
        IDLE = 0,
        MOVING,
        SHOOTINGNORMAL,
        SHOOTINGRADIAL,
        SHOOTINGEXPLODINGRADIAL,
        SHOOTINGMISSLE,
        SHOOTINGEXPLODING,
        SPAWNMINES,
        SPAWNSPIDERS,
        CHARGING,
        HURT,
        TOTAL
    };

    AOEBulletController bossBulletMovement;
    ExplodingBullet bossExplodingBullet;
    GameObject Player;
    NavMeshAgent bossNavAgent;
    NavMeshPath bossPath;

    [Header ("Boss Setting")]
    public float bossHealth = 100.0f;

    [Header ("Bullet Settings")]
    public GameObject normalBulletPrefab;
    public GameObject explodingBulletPrefab;
    public GameObject spiderMinesPrefab;
    public GameObject bulletSpawner;
    
    public int numberOfBullets;
    public float aimingDamping = 2.0f;
    public float sectionSize = 10.0f;
    public float angleToPlayer;
    public float bulletSpeed = 10.0f;
    public float shootTimeNormal = 1.0f;
    public float shootTimeRadial = 2.0f;
    public float secondRadialOffset = 10.0f;
    float curShoot = 0.0f;
    public float curGlobalTime = 0.0f;
    public float curGlobalTime2 = 0.0f;
    float counter = 0.0f;
    float changeStateTime = 10.0f;

    private const float radius = 1f;
    [Header("Boss States")]
    public BossCurrentState curState;
    public BossCurrentState curState2;
    public BossCurrentState preState;
    public float firstStageSwitchTime = 10.0f;
    public float secondStageSwitchTime = 10.0f;
    public float finalStageSwitchTime = 15.0f;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
        curState = BossCurrentState.IDLE;


        bossNavAgent = this.GetComponent<NavMeshAgent>();
        bossPath = new NavMeshPath();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        switch (curState)
        {
            case BossCurrentState.IDLE:
                checkCurrentHealth();
                break;

            case BossCurrentState.SHOOTINGNORMAL:
                MoveToPlayer();
                ShootAtPlayer();
                checkCurrentHealth();
                break;

            case BossCurrentState.SHOOTINGRADIAL:
                MoveToPlayer();
                ShootRadial();
                checkCurrentHealth();
                break;

            case BossCurrentState.SHOOTINGEXPLODINGRADIAL:
                ShootExplodingRadial();
                checkCurrentHealth();
                break;

            case BossCurrentState.SHOOTINGMISSLE:
                StartMissle();
                checkCurrentHealth();
                break;

            case BossCurrentState.SPAWNSPIDERS:
                SpawnSpiders();
                break;
        }
    }

    void checkCurrentHealth()
    {
        curGlobalTime += Time.deltaTime;
        

        if (curState == BossCurrentState.IDLE)
        {
            preState = curState;
            curState = BossCurrentState.SHOOTINGNORMAL;
        }

        if (bossHealth >= 70 && curGlobalTime >= firstStageSwitchTime)
        {
            if (curState == BossCurrentState.SHOOTINGNORMAL)
            {
                preState = curState;
                curState = BossCurrentState.SHOOTINGRADIAL;
                shootTimeNormal = 0.5f;
                curGlobalTime = 0.0f;
            }
            else if (curState == BossCurrentState.SHOOTINGRADIAL)
            {
                preState = curState;
                curState = BossCurrentState.SHOOTINGNORMAL;
                shootTimeRadial = 0.5f;
                curGlobalTime = 0.0f;
            }
            
        }
            
         if (bossHealth < 70 && bossHealth >= 40)
        {
            WallScript.Instance.curState = WallScript.StageState.STAGE2;
            GetComponent<NavMeshAgent>().speed = 3.0f;
            if (curGlobalTime >= secondStageSwitchTime)
            {
                preState = curState;
                curState = BossCurrentState.SHOOTINGEXPLODINGRADIAL;
                curGlobalTime = 0.0f;
            }
            
        }

         if (bossHealth < 40 && bossHealth > 0)
        {
            curGlobalTime2 += Time.deltaTime;
            if (curGlobalTime2 >= secondStageSwitchTime)
            {
                preState = curState;
                curState = BossCurrentState.SHOOTINGEXPLODINGRADIAL;
                curGlobalTime2 = 0.0f;
            }
            if (curGlobalTime >= finalStageSwitchTime)
            {
                preState = curState;
                curState = BossCurrentState.SPAWNSPIDERS;
                curGlobalTime = 0.0f;
            }
        }

         if (bossHealth < 0)
        {
            LoadScenes.Instance.LoadMainMenu();
        }
        
    }

    void CheckGameStatus()
    {
        preState = curState;
        curState = BossCurrentState.SHOOTINGRADIAL;

    }

    void MoveToPlayer()
    {
        bossNavAgent.SetDestination(Player.transform.position);
    }

    void ShootAtPlayer()
    {
        
        curShoot += Time.deltaTime;

        Vector3 toOther = (this.transform.position - Player.transform.position).normalized;

        angleToPlayer = Mathf.Atan2(toOther.z, toOther.x) * Mathf.Rad2Deg + 180;

        Quaternion rotation = Quaternion.LookRotation(new Vector3(Player.transform.position.x, 0.0f, Player.transform.position.z) - new Vector3(bulletSpawner.transform.position.x, 0.0f, bulletSpawner.transform.position.z));
        bulletSpawner.transform.rotation = Quaternion.Slerp(bulletSpawner.transform.rotation, rotation, Time.deltaTime * aimingDamping);
        //bulletSpawner.transform.LookAt(new Vector3 (Player.transform.position.x , bulletSpawner.transform.position.y, Player.transform.position.z));

        if (curShoot >= shootTimeNormal)
        {
            Vector3 randomShots = new Vector3(Random.Range(0.0f, 4.0f), 0.0f, Random.Range(0.0f, 1.5f));
            GameObject tmpObj = Instantiate(normalBulletPrefab, bulletSpawner.transform.position + randomShots, bulletSpawner.transform.rotation);
            tmpObj.GetComponent<Rigidbody>().velocity = tmpObj.transform.forward * bulletSpeed;
            curShoot = 0;
            shootTimeNormal = Random.Range(0.1f, 0.3f);
        }
        
    }

    void ShootRadial()
    {
        bulletSpawner.transform.LookAt(new Vector3(Player.transform.position.x, bulletSpawner.transform.position.y, Player.transform.position.z));
        curShoot += Time.deltaTime;
        float radialOffset = 80.0f;
        if (curShoot >= shootTimeRadial && counter <= 0)
        {
            float anglestep = 180 / numberOfBullets;
            float angle = bulletSpawner.transform.eulerAngles.y - radialOffset;
            spawnRadialBullet(angle, anglestep);
            curShoot = 0.0f;
            shootTimeRadial = 0.3f;
            counter += 1;
        }
        else if (curShoot >= shootTimeRadial && counter > 0)
        {
            float anglestep = 180 / numberOfBullets;
            float angle = bulletSpawner.transform.eulerAngles.y - (radialOffset - secondRadialOffset);
            spawnRadialBullet(angle, anglestep);
            curShoot = 0.0f;
            shootTimeRadial = 2.0f;
            counter = 0;
        }

    }

    void ShootExplodingRadial()
    {
        bulletSpawner.transform.LookAt(new Vector3(Player.transform.position.x, bulletSpawner.transform.position.y, Player.transform.position.z));

        GameObject tmpObj = Instantiate(explodingBulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
        tmpObj.GetComponent<Rigidbody>().velocity = tmpObj.transform.forward * bulletSpeed;

        preState = curState;
        curState = BossCurrentState.SHOOTINGNORMAL;
        shootTimeNormal = 0.5f;
        
    }

    void spawnRadialBullet(float angle, float anglestep)
    {
        for (int i = 0; i < numberOfBullets; ++i)
        {
            float projectileX = bulletSpawner.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileZ = bulletSpawner.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileX, projectileZ, 0.0f);
            Vector3 bulletMoveDirection = (projectileVector - this.transform.position);

            GameObject tmpObj = Instantiate(normalBulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y).normalized * bulletSpeed;

            angle += anglestep;
        }
    }

    void StartMissle()
    {

    }

    void SpawnMines()
    {

    }

    void SpawnSpiders()
    {
        if (GameObject.FindGameObjectsWithTag("Destroyable").Length <= 3)
        {
            Instantiate(spiderMinesPrefab, this.transform.position, Quaternion.identity);
        }
        else
        {
            preState = curState;
            curState = BossCurrentState.SHOOTINGNORMAL;
        }
        
    }
    
}
