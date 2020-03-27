using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dray : MonoBehaviour, IFacingMover
{
    public enum eMode { idle, move, attack, transition}

    [Header("Set in Inspector")]
    public float speed = 5;
    public float attackDuration = 0.25f;
    public float attackDelay = 0.5f;

    [Header("Set Dynamically")]
    public int dirHeld = -1;        // direction held by user of movement key
    public int facing = 1;          // direction Dray is facing
    public eMode mode = eMode.idle;

    private float timeAtkDone = 0;
    private float timeAtkNext = 0;

    private Rigidbody rigid;
    private Animator anim;
    private InRoom inRm;

    private Vector3[] directions = new Vector3[]
    {
        Vector3.right, Vector3.up, Vector3.left, Vector3.down
    };

    private KeyCode[] keys = new KeyCode[]
    {
        KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow
    };
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        inRm = GetComponent<InRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        dirHeld = -1;

        for (int i=0; i < 4; i++)
        {
            if (Input.GetKey(keys[i])) dirHeld = i;
        }
        // Press the attack button
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= timeAtkNext)
        {
            mode = eMode.attack;
            timeAtkDone = Time.time + attackDuration;
            timeAtkNext = Time.time + attackDelay;
        }

        // finish our attach when it's over
        if (Time.time >= timeAtkDone)
        {
            mode = eMode.idle;
        }

        // choosing the proper mode if we're not attacking
        if (mode != eMode.attack)
        {
            if (dirHeld == -1)
            {
                mode = eMode.idle;
            }
            else
            {
                facing = dirHeld;
                mode = eMode.move;
            }
        }

        // act on the current mode
        switch (mode)
        {
            case eMode.attack:
                anim.CrossFade("Dray_Attack_" + facing, 0);
                anim.speed = 0;
                break;
            case eMode.idle:
                anim.CrossFade("Dray_Walk_" + facing, 0);
                anim.speed = 0;
                break;
            case eMode.move:
                anim.CrossFade("Dray_Walk_" + facing, 0);
                anim.speed = 1;
                break;
        }


        Vector3 vel = Vector3.zero;

        if (dirHeld > -1) vel = directions[dirHeld];

        rigid.velocity = vel * speed;

        // animation
        if (dirHeld == -1)
        {
            anim.speed = 0;
        }
        else
        {
            anim.CrossFade("Dray_Walk_" + dirHeld, 0);
            anim.speed = 1;
        }

    }

    // implementation of IFacingMover

    public int GetFacing()
    {
        return facing;
    }
    public bool moving
    {
        get
        {
            return (mode == eMode.move);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float gridMult
    {
        get { return inRm.gridMult; }
    }
    public Vector2 roomPos
    {
        get { return inRm.roomPos; }
        set { inRm.roomPos = value; }
    }
    public Vector2 roomNum
    {
        get { return inRm.roomNum; }
        set { inRm.roomNum = value; }
    }
    public Vector2 GetRoomPosOnGrid( float mult = -1 )
    {
        return inRm.GetRoomPosOnGrid(mult);
    }
}
