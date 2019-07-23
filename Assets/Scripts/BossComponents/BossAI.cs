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
        CUTSCENE,
        MOVING,
        SHOOTINGNORMAL,
        SHOOTINGRADIAL,
        SHOOTING360SHOT,
        SHOOTINGEXPLODINGRADIAL,
        SHOOTINGMISSLE,
        SHOOTINGEXPLODING,
        SPAWNMINES,
        SPAWNSPIDERS,
        HURT,
        DYING,
        TOTAL
    };

    AOEBulletController bossBulletMovement;
    ExplodingBullet bossExplodingBullet;
    GameObject Player;
    NavMeshAgent bossNavAgent;
    NavMeshPath bossPath;

    [Header ("Boss Setting")]
    public float maxbossHealth = 100.0f;
    public float bossHealth = 0.0f;
    public SpriteRenderer bossSprite;

    [Header ("Bullet Settings")]
    public GameObject normalBulletPrefab;
    public GameObject destroyableBulletPrefab;
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

    public float spinSpeed = 10.0f;
    public float spinTime = 20.0f;

    public float RadialDestroyable = 2.0f;

    float curShoot = 0.0f;
    public float curGlobalTime = 0.0f;
    public float curGlobalTime2 = 0.0f;
    float counter = 0.0f;
    float changeStateTime = 10.0f;

    float hurtFlashTime = 0.1f;
    float curFlashTime = 0.0f;
    bool hurtFlash = false;
    bool isFacingPlayer = false;
    float fullCircleTimer = 0;

    private const float radius = 1f;

    [Header("Boss States")]
    public BossCurrentState curState;
    public BossCurrentState curState2;
    public BossCurrentState preState;
    public float firstStageSwitchTime = 10.0f;
    public float secondStageSwitchTime = 10.0f;
    public float second2ndStageSwitchTime = 10.0f;
    public float finalStageSwitchTime = 15.0f;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
        //curState = BossCurrentState.IDLE;

        bossHealth = maxbossHealth;

        bossNavAgent = this.GetComponent<NavMeshAgent>();
        bossPath = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
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

            case BossCurrentState.SHOOTING360SHOT:
                MoveToPlayer();
                Shoot360();
                checkCurrentHealth();
                break;

            case BossCurrentState.SHOOTINGEXPLODINGRADIAL:
                ShootExplodingRadial();
                checkCurrentHealth();
                break;

            case BossCurrentState.SHOOTINGMISSLE:
                //StartMissle();
                checkCurrentHealth();
                break;

            case BossCurrentState.SPAWNSPIDERS:
                SpawnSpiders();
                break;
        }

        if (hurtFlash)
            bossFlash();
    }

    void checkCurrentHealth()
    {
        curGlobalTime += Time.deltaTime;

        float currentPercent = bossHealth / maxbossHealth * 100.0f;

        if (curState == BossCurrentState.IDLE)
        {
            preState = curState;
            curState = BossCurrentState.SHOOTINGNORMAL;
        }

        if (currentPercent >= 70 && curGlobalTime >= firstStageSwitchTime)
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
            else if (curState == BossCurrentState.SHOOTING360SHOT)
            {

            }
            
        }
            
         if (currentPercent < 70 && currentPercent >= 40)
        {
            curGlobalTime2 += Time.deltaTime;
            WallScript.Instance.curState = WallScript.StageState.STAGE2;
            GetComponent<NavMeshAgent>().speed = 3.0f;
            if (curGlobalTime >= secondStageSwitchTime)
            {
                preState = curState;
                curState = BossCurrentState.SHOOTINGEXPLODINGRADIAL;
                curGlobalTime = 0.0f;
            }
            if (curGlobalTime2 >= second2ndStageSwitchTime)
            {
                preState = curState;
                curState = BossCurrentState.SHOOTING360SHOT;
                curGlobalTime2 = 0.0f;
            }
        }

         if (currentPercent < 40 && currentPercent > 0)
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

         if (currentPercent <= 0)
        {
            LoadScenes.Instance.afterGame();
            GameManager.Instance.curState = CurrentGameState.GameWon;
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
            SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_BOSS_SHOOT);
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

            if ((i % RadialDestroyable) == 0)
            {
                GameObject destObj = Instantiate(destroyableBulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
                destObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y).normalized * bulletSpeed;
                destObj.GetComponent<BossNormalBullet>().isDestroyable = true;
            }
            else
            {
                GameObject tmpObj = Instantiate(normalBulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
                tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y).normalized * bulletSpeed;
                
            }

            angle += anglestep;
        }
        SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_BOSS_SCATTER);
    }

    void Shoot360()
    {
        

        if (!isFacingPlayer)
        {
            bulletSpawner.transform.LookAt(new Vector3(Player.transform.position.x, bulletSpawner.transform.position.y, Player.transform.position.z));
            isFacingPlayer = true;
        }
        else if(isFacingPlayer)
        {
            fullCircleTimer += Time.deltaTime;

            if (fullCircleTimer < spinTime)
            {
                bulletSpawner.transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed, Space.World);

                GameObject tmpObj = Instantiate(normalBulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
                tmpObj.GetComponent<Rigidbody>().velocity = bulletSpawner.transform.forward * bulletSpeed;
            }
            else if (fullCircleTimer >= spinTime)
            {
                isFacingPlayer = false;
                fullCircleTimer = 0.0f;
                curState = BossCurrentState.SHOOTINGNORMAL;
                preState = curState;
            }
            
             
        }

       
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

    public void bossTakeDamage(float damage)
    {
        hurtFlash = true;
        bossHealth -= damage;
        SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_BOSS_HIT);
    }
    
    void bossFlash()
    {
        curFlashTime += Time.deltaTime;

        bossSprite.color = Color.red;

        if(curFlashTime >= hurtFlashTime)
        {
            bossSprite.color = Color.white;
            curFlashTime = 0.0f;
            hurtFlash = false;
        }
    }
}
