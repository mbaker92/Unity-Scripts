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
    public Transform[] PathSequence;

    // Draw path lines in editor to show the path
    public void OnDrawGizmos()
    {
        if(PathSequence == null || PathSequence.Length < 2)
        {
            return;
        }
        for(var i=1;i<PathSequence.Length; i++)
        {
            Gizmos.DrawLine(PathSequence[i - 1].position, PathSequence[i].position);
        }

        if(Pathtype == PathTypes.loop)
        {
            Gizmos.DrawLine(PathSequence[0].position, PathSequence[PathSequence.Length - 1].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (PathSequence == null || PathSequence.Length < 1)
        {
            yield break; // exit coroutine sequence length check fails.
        }
        while (true)
        {
            // return current point in pathsequence
            yield return PathSequence[movingTo];

            // if only one point exit coroutine.
            if(PathSequence.Length == 1)
            {
                continue;
            }
            // if linear
            if (Pathtype == PathTypes.linear)
            {
                if(movingTo <= 0)
                {
                    movementDirection = 1;
                }else if(movingTo >= PathSequence.Length - 1)
                {
                    movementDirection = -1;
                }
            }
            movingTo = movingTo + movementDirection;
            if(Pathtype == PathTypes.loop)
            {
                if (movingTo >= PathSequence.Length)
                {
                    movingTo= 0;
                }
                if (movingTo < 0)
                {
                    movingTo = PathSequence.Length - 1;
                }
                }
            }
        }
    }
