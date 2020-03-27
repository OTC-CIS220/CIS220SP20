using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour
{
    private IFacingMover mover;

    private void Awake()
    {
        mover = GetComponent<IFacingMover>();
    }

    private void FixedUpdate()
    {
        if (!mover.moving) return;  // if not moving, nothing to do
        int facing = mover.GetFacing();

        // if we are moving in a direction, align to the grid
        // First, get the grid location
        Vector2 rPos = mover.roomPos;
        Vector2 rPosGrid = mover.GetRoomPosOnGrid();
        // this relies on IFacingMover (which uses InRoom) to choose grid spacing
        // then move towards the grid line
        float delta = 0;
        if (facing == 0 || facing == 2)
        {
            // horizontal movement, align to y grid
            delta = rPosGrid.y - rPos.y;
        } else
        {
            // vertical movement, align to x grid
            delta = rPosGrid.x - rPos.x;
        }
        if (delta == 0) return;

        float move = mover.GetSpeed() * Time.fixedDeltaTime;
        move = Mathf.Min(move, Mathf.Abs(delta));
        if (delta < 0) move = -move;
        if (facing == 0 || facing == 2)
        {
            // horizontal movement, align to y grid
            rPos.y += move;
        } else
        {
            // vertical movement, align to x grid
            rPos.x += move;
        }

        mover.roomPos = rPos;
    }
}
