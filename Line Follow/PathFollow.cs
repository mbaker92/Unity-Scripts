using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour {

    public enum PathTypes
    {
        linear,
        loop
    }

    public PathTypes Pathtype;
    public int movementDirection = 1;
    public int movingTo = 0;
    public Transform[] Path;

    // Draw path lines in editor to show the path
    public void OnDrawGizmos()
    {
        // If there is no path return
        if(Path == null || Path.Length < 2)
        {
            return;
        }

        // Draw a line between points in the editor
        for(int i=1;i<Path.Length; i++)
        {
            Gizmos.DrawLine(Path[i - 1].position, Path[i].position);
        }

        // If the path loops, then draw a line from first position to the last position
        if(Pathtype == PathTypes.loop)
        {
            Gizmos.DrawLine(Path[0].position, Path[Path.Length - 1].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (Path == null || Path.Length < 1)
        {
            yield break; // exit coroutine sequence length check fails.
        }
        while (true)
        {
            // return current point in pathsequence
            yield return Path[movingTo];

            // if only one point exit coroutine.
            if(Path.Length == 1)
            {
                continue;
            }
            // if linear
            if (Pathtype == PathTypes.linear)
            {
                if(movingTo <= 0)
                {
                    movementDirection = 1;
                }else if(movingTo >= Path.Length - 1)
                {
                    movementDirection = -1;
                }
            }
            movingTo = movingTo + movementDirection;

            // If Looping
            if(Pathtype == PathTypes.loop)
            {
                if (movingTo >= Path.Length)
                {
                    movingTo= 0;
                }
                if (movingTo < 0)
                {
                    movingTo = Path.Length - 1;
                }
                }
            }
        }
    }
