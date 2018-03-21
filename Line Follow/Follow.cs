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
            Debug.LogError("Need a path to follow");
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
        if (type == MovementType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointsinPath.Current.position, Time.deltaTime * speed);
        }
        else if(type == MovementType.LerpTowards)
        {
            transform.position = Vector3.Lerp(transform.position, pointsinPath.Current.position, Time.deltaTime * speed);
        }


        var distanceSquared = (transform.position - pointsinPath.Current.position).sqrMagnitude;
        if( distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointsinPath.MoveNext();
        }
	}
}
