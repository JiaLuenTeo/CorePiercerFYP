using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [SerializeField] private Transform DashPrefab;

    public float speed;
    public float dashDistance;
    public float dashTime = 1.0f;
    Rigidbody rb;
    float translationX, translationY;
    public float dashCurTime = 10.0f;
    public Animator anim;
    private bool isMoving;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.Instance.cutScene)
            dashMovement();
        playerAnimation();
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.cutScene)
            keyboardMovement();
    }

    void keyboardMovement()
    {
        translationY = Input.GetAxisRaw("Vertical") * speed;
        translationX = Input.GetAxisRaw("Horizontal") * speed;

        transform.Translate(new Vector3(translationX, 0f, translationY) * Time.deltaTime);
    }

    void dashMovement()
    {
        dashCurTime += Time.deltaTime;
        Vector3 currentDirection = new Vector3(translationX, 0.0f, translationY);
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space) && currentDirection != Vector3.zero)
        {
            Debug.Log("Button Pressed");
            //float buttonPressed = curTime;
            //play start up animation here

            //get button that is being pressed here for direction (y is set to the current player Y. Only moves in the XZ)
            
    
            if (dashCurTime >= dashTime)
            {
                PlayerManager.Instance.isInvincible = true;
                //Play dash animation here or something lol
                Vector3 beforeDashPosition = transform.position;
                Transform dashTransform = Instantiate(DashPrefab, beforeDashPosition, Quaternion.identity);
                dashTransform.eulerAngles = new Vector3(65, 0, GetAngleFromVectorFloat(beforeDashPosition));
                this.GetComponent<Rigidbody>().AddForce(currentDirection.normalized * dashDistance,ForceMode.Impulse);
                Debug.Log("Currently Dashing");
                dashCurTime = 0.0f;
            }
            
            
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if(dashCurTime <= 0)
        PlayerManager.Instance.isInvincible = false;
    }

    void playerAnimation()
    {
        Vector3 currentDirection = new Vector3(translationX, 0.0f, translationY);
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (translationX == 0 && translationY == 0)
        {
            anim.speed = 1;
            anim.SetInteger("Direction", 0);
        }
        else
        {
            if (Input.GetKey(KeyCode.W)&& mousepos.z > 0.1)
            {
                anim.speed = 1;
                anim.SetInteger("Direction", 1);
            }

            if (mousepos.x > 0.1 && mousepos.z > 0.1)
            {
                anim.speed = 1;
                anim.SetInteger("Direction", 2);
            }

            if (mousepos.x > 0.1 && Input.GetKey(KeyCode.D))
            {
                anim.speed = 1;
                anim.SetInteger("Direction", 3);
            }

            if (mousepos.x > 0.1 && mousepos.z < -0.1)
            {
                anim.speed = 1;
                anim.SetInteger("Direction", 4);
            }
            if (Input.GetKey(KeyCode.S) && mousepos.z < -0.1)
            {
                anim.speed = 1;
                anim.SetInteger("Direction", 5);
            }
            if (mousepos.x < -1 && mousepos.z < -1)
            {
                anim.speed = 1;
                anim.SetInteger("Direction", 6);
            }
            if (mousepos.x < -1 && Input.GetKey(KeyCode.A))
            {
                anim.speed = 1;
                anim.SetInteger("Direction", 7);
            }
            if (mousepos.x < -1 && mousepos.z > 1)
            {
                anim.speed = 1;
                anim.SetInteger("Direction", 8);
            }

            if (mousepos.x == 0 && mousepos.z == 0)
            {
                anim.speed = 1;
                anim.SetInteger("Direction", 0);
            }

        }


        Debug.Log(mousepos);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Walls")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
