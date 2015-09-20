using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerScript : MonoBehaviour
{

    BetterPathFinder betterPathFinder;
    public int speed;
	public int[] dequeuedPath;
    public Queue<int[]> reversedDequeuedPath;
    public GameObject findThis;

    void Start()
    {
        betterPathFinder = GetComponent<BetterPathFinder>();
        reversedDequeuedPath = new Queue<int[]>();
        betterPathFinder.pathToFollow = new Stack<int[]>();
		dequeuedPath = new int[] {
			(int)Mathf.Floor (this.transform.position.x),
			(int)Mathf.Floor (this.transform.position.z)
		};
    }

    void Update()
    {
        MoveTowardsLocation();

        if (Input.GetMouseButtonDown(1))
        {
			this.transform.position = new Vector3(Mathf.Floor(this.transform.position.x)+0.5f,Mathf.Floor(this.transform.position.y)+0.5f,Mathf.Floor(this.transform.position.z)+0.5f);
            reversedDequeuedPath = new Queue<int[]>();
            dequeuedPath = new int[2];
            betterPathFinder.pathToFollow = new Stack<int[]>();

            Vector3 screenPointToWorld = (GetWorldPositionOnPlane(Input.mousePosition, 100));
            findThis.transform.position = new Vector3(Mathf.Floor(screenPointToWorld.x) + .5f, this.transform.position.y, Mathf.Floor(screenPointToWorld.z) + .5f);
            Debug.Log(findThis.transform.position);

            betterPathFinder.createPathfinder();

            for (int i = betterPathFinder.pathToFollow.Count - 1; i >= 0; i--)
            {
                reversedDequeuedPath.Enqueue(betterPathFinder.pathToFollow.Pop());
            }
			reversedDequeuedPath.Enqueue(new int[] {(int)Mathf.Floor(findThis.transform.position.x),(int)Mathf.Floor(findThis.transform.position.z)});
            if (reversedDequeuedPath.Count != 0)
            {
                dequeuedPath = reversedDequeuedPath.Dequeue();
                Debug.Log(dequeuedPath[0] + ", " + dequeuedPath[1]);
			}
        }
    }

    public void MoveTowardsLocation()
    {
        Debug.Log(dequeuedPath[0] + ", " + dequeuedPath[1]);

        if (dequeuedPath.Length > 0)
        {
            if (this.transform.position != new Vector3(dequeuedPath[0] + .5f, this.transform.position.y, dequeuedPath[1] + .5f))
            {
                float stepSpeed = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(this.transform.position, new Vector3((int)dequeuedPath[0] + .5f, this.transform.position.y, (int)dequeuedPath[1] + .5f), stepSpeed);
            }
            else if (reversedDequeuedPath.Count > 0)
            {
                dequeuedPath = reversedDequeuedPath.Dequeue();
            }
        }
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
