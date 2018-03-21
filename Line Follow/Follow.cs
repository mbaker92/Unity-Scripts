using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public enum MovementType
    {
        MoveTowards,
        LerpTowards
    }

    public MovementType type = MovementType.MoveTowards;
    public PathFollow MyPath;
    public float speed = 1;
    public float MaxDistanceToGoal = .1f;

    private IEnumerator<Transform> pointsinPath;

	// Use this for initialization
	void Start () {
		if (MyPath == null)
        {
           
            // No Path
            return;
        }

        pointsinPath = MyPath.GetNextPathPoint();

        pointsinPath.MoveNext();

        if(pointsinPath.Current == null)
        {
            Debug.LogError("Need a point in the path", gameObject);
            return;

        }

        transform.position = pointsinPath.Current.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (pointsinPath == null || pointsinPath.Current == null)
        {
            return;
        }
        // Move towards point at steady speed
        if (type == MovementType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointsinPath.Current.position, Time.deltaTime * speed);
        }
        // Move towards point with Lerp
        else if(type == MovementType.LerpTowards)
        {
            transform.position = Vector3.Lerp(transform.position, pointsinPath.Current.position, Time.deltaTime * speed);
        }


        var distanceSquared = (transform.position - pointsinPath.Current.position).sqrMagnitude;

        // Go to next point if close enough to point.
        if( distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointsinPath.MoveNext();
        }
	}
}
