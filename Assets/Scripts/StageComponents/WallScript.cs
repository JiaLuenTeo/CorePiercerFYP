using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private static WallScript mInstance = null;
    public static WallScript Instance;


    public enum StageState
    {
        IDLE = 0,
        STAGE1,
        STAGE2,
        STAGE3,
        WIN,
        TOTAL
    };


    public StageState curState;
    public StageState preState;
    public Transform middleDown;
    public Transform middleUp;
    public Transform wall12;
    public Transform wall16;
    public Transform crossWall;
    public Transform crossWall_;
    public Transform cylinder;
    public Transform cylinder1;
    public Transform cylinder2;
    public Transform cylinder3;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        curState = StageState.IDLE;

    }



    void FixedUpdate()
    {
        switch (curState)
        {
            case StageState.IDLE:
                CheckStageStatus();
                break;

            case StageState.STAGE1:

                break;

            case StageState.STAGE2:
                MiddleWallUp();
                tiles12Down();
                break;

            case StageState.STAGE3:
                tiles16Down();
                MiddleWallDown();
                CrossWallUp();
                CylinderUp();

                break;

            case StageState.WIN:

                break;
        }
    }

    void CheckStageStatus()
    {
        preState = curState;
        curState = StageState.STAGE1;

    }

    void MiddleWallUp()
    {
        middleDown = this.gameObject.transform.Find("WallMiddleDown");
        if (middleDown.position.y <= 1.4f)
        {
            middleDown.Translate(0, 0.1f, 0);
        }
        else
        {
            middleDown.Translate(0, 0f, 0);
        }

        middleUp = this.gameObject.transform.Find("WallMiddleUp");
        if (middleUp.position.y <= 1.4f)
        {
            middleUp.Translate(0, 0.1f, 0);
        }
        else
        {
            middleUp.Translate(0, 0f, 0);
        }
    }


    void MiddleWallDown()
    {
        middleDown = this.gameObject.transform.Find("WallMiddleDown");
        if (middleDown.position.y > -1.9f)
        {
            middleDown.Translate(0, -0.1f, 0);
        }
        else
        {
            middleDown.Translate(0, 0f, 0);
        }

        middleUp = this.gameObject.transform.Find("WallMiddleUp");
        if (middleUp.position.y > -1.9f)
        {
            middleUp.Translate(0, -0.1f, 0);
        }
        else
        {
            middleUp.Translate(0, 0f, 0);
        }
    }

    void CrossWallUp()
    {
        crossWall = this.gameObject.transform.Find("Crosswall");
        if (crossWall.position.y <= 1.4f)
        {
            crossWall.Translate(0, 0.1f, 0);
        }
        else
        {
            crossWall.Translate(0, 0f, 0);
        }

        crossWall_ = this.gameObject.transform.Find("Crosswall (1)");
        if (crossWall_.position.y <= 1.4f)
        {
            crossWall_.Translate(0, 0.1f, 0);
        }
        else
        {
            crossWall_.Translate(0, 0f, 0);
        }
    }

    void CrossWallDown()
    {
        crossWall = this.gameObject.transform.Find("Crosswall");
        if (crossWall.position.y > -1.9f)
        {
            crossWall.Translate(0, -0.1f, 0);
        }
        else
        {
            crossWall.Translate(0, 0f, 0);
        }

        crossWall_ = this.gameObject.transform.Find("Crosswall(1)");
        if (crossWall_.position.y > -1.9f)
        {
            crossWall_.Translate(0, -0.1f, 0);
        }
        else
        {
            crossWall_.Translate(0, 0f, 0);
        }
    }

    void tiles12Up()
    {
        wall12 = this.gameObject.transform.Find("Wall12");
        if (wall12.position.y <= 1.4f)
        {
            wall12.Translate(0, 0.1f, 0);
        }
        else
        {
            wall12.Translate(0, 0f, 0);
        }
    }


    void tiles12Down()
    {
        wall12 = this.gameObject.transform.Find("Wall12");
        if (wall12.position.y > -1.9f)
        {
            wall12.Translate(0, -0.1f, 0);
        }
        else
        {
            wall12.Translate(0, 0f, 0);
        }
    }

    void tiles16Up()
    {
        wall16 = this.gameObject.transform.Find("Wall16");
        if (wall16.position.y <= 1.4f)
        {
            wall16.Translate(0, 0.1f, 0);
        }
        else
        {
            wall16.Translate(0, 0f, 0);
        }
    }


    void tiles16Down()
    {
        wall16 = this.gameObject.transform.Find("Wall16");
        if (wall16.position.y > -1.9f)
        {
            wall16.Translate(0, -0.1f, 0);
        }
        else
        {
            wall16.Translate(0, 0f, 0);
        }
    }



    void CylinderUp()
    {
        cylinder = this.gameObject.transform.Find("Cylinder");
        if (cylinder.position.y <= 1.4f)
        {
            cylinder.Translate(0, 0.1f, 0);
        }
        else
        {
            cylinder.Translate(0, 0f, 0);
        }

        cylinder1 = this.gameObject.transform.Find("Cylinder (1)");
        if (cylinder1.position.y <= 1.4f)
        {
            cylinder1.Translate(0, 0.1f, 0);
        }
        else
        {
            cylinder1.Translate(0, 0f, 0);
        }

        cylinder2 = this.gameObject.transform.Find("Cylinder (2)");
        if (cylinder2.position.y <= 1.4f)
        {
            cylinder2.Translate(0, 0.1f, 0);
        }
        else
        {
            cylinder2.Translate(0, 0f, 0);
        }

        cylinder3 = this.gameObject.transform.Find("Cylinder (3)");
        if (cylinder3.position.y <= 1.4f)
        {
            cylinder3.Translate(0, 0.1f, 0);
        }
        else
        {
            cylinder3.Translate(0, 0f, 0);
        }
    }

    void CylinderDown()
    {
        cylinder = this.gameObject.transform.Find("Cylinder");
        if (cylinder.position.y > -1.9f)
        {
            cylinder.Translate(0, -0.1f, 0);
        }
        else
        {
            cylinder.Translate(0, 0f, 0);
        }

        cylinder1 = this.gameObject.transform.Find("Cylinder(1)");
        if (cylinder1.position.y > -1.9f)
        {
            cylinder1.Translate(0, -0.1f, 0);
        }
        else
        {
            cylinder1.Translate(0, 0f, 0);
        }

        cylinder2 = this.gameObject.transform.Find("Cylinder(2)");
        if (cylinder2.position.y > -1.9f)
        {
            cylinder2.Translate(0, -0.1f, 0);
        }
        else
        {
            cylinder2.Translate(0, 0f, 0);
        }

        cylinder3 = this.gameObject.transform.Find("Cylinder(3)");
        if (cylinder3.position.y > -1.9f)
        {
            cylinder3.Translate(0, -0.1f, 0);
        }
        else
        {
            cylinder3.Translate(0, 0f, 0);
        }
    }





    void Update()
    {

    }


}
