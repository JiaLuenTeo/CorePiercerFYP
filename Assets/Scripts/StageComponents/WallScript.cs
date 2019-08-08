using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private static WallScript mInstance = null;
    public static WallScript Instance;


    public enum StageState
    {
        STAGE1 = 0,
        STAGE2,
        STAGE3,
        WIN,
        TOTAL
    };


    public StageState curState;
    public StageState preState;
    public Transform StageOne;
    public Transform StageTwo;
    public Transform StageTwoWalls;
    public Transform StageThree;


    public float wallRiseSpeed = 10.0f;
    public float wallFallSpeed = 10.0f;
    

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        curState = StageState.STAGE1;
        preState = curState;
    }



    void FixedUpdate()
    {
        switch (curState)
        {
            case StageState.STAGE2:
                changeStage("Stage 2");
                break;

            case StageState.STAGE3:
                changeStage("Stage 3");
                break;

            case StageState.WIN:

                break;
        }
    }

    void changeStage(string curStage)
    {
        if (curStage == "Stage 2")
        {
            if(StageOne.position.y > -4.0f)
                StageOne.Translate(Vector3.down * wallFallSpeed * Time.deltaTime);
            if (StageTwo.position.y <= 7.0f)
                StageTwo.Translate(Vector3.up * wallRiseSpeed * Time.deltaTime);
            preState = curState;
        }
        else if (curStage == "Stage 3")
        {
            if (StageTwo.position.y > -4.0f)
            {
                StageTwo.Translate(Vector3.down * wallRiseSpeed * Time.deltaTime);
                StageTwoWalls.Translate(Vector3.down * wallRiseSpeed * Time.deltaTime);
            }
            if (StageThree.position.y <= 7.0f)
                StageThree.Translate(Vector3.up * wallRiseSpeed * Time.deltaTime);

            preState = curState;
        }
    }


    /*void MiddleWallUp()
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



    */


}
