using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathfinderHandler : MonoBehaviour
{
    public GameObject objectToBeSpawned = null;
    public int max = 20;
    public int[,] pathLayer;
    public int x = 2;
    public Vector3 thisPos;
    public Queue<int[]> toDO;

    // Initialization
    void Start()
    {
        toDO = new Queue<int[]>();
        this.thisPos = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        int[] next = new int[2];
        next[0] = max;
        next[1] = max;
        toDO.Enqueue(next);

        CreatePathfinderGrid();
    }

    // Update
    void Update()
    {
    }

    public void CreatePathfinderGrid()
    {
        this.pathLayer = new int[(this.max * 2), (this.max * 2)];

        this.pathLayer[this.max, this.max] = -1;

        /*
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject gameObj in objects)
        {
            pathLayer[(int)gameObj.transform.position.x + this.max, (int)gameObj.transform.position.z + this.max] = -1;

            objectToBeSpawned = Instantiate(Resources.Load("Prefabs/Block"), (new Vector3((int)gameObj.transform.position.x + this.max, 0, (int)gameObj.transform.position.z + this.max)), Quaternion.identity) as GameObject;
            objectToBeSpawned.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            objectToBeSpawned.name = "1";
        }
         */

        while (true)
        {
            if (toDO.Count == 0)
            {
                break;
            }

            int[] next2 = (int[])toDO.Dequeue();
            FourWaySpawnAndLog(next2[0], next2[1]);
        }

        Debug.Log("Done!");

    }

    public void FourWaySpawnAndLog(int i, int j)
    {
        int nextDistance = pathLayer[i, j] + 1;

        EnqueNext(i, j + 1, nextDistance);
        EnqueNext(i - 1, j, nextDistance);
        EnqueNext(i, j - 1, nextDistance);
        EnqueNext(i + 1, j, nextDistance);
    }

    void EnqueNext(int i, int j, int nextDistance)
    {
        if (i <= 0 || i >= max * 2 || j <= 0 || j >= max * 2)
        {
            return;
        }

        Vector3 spawnAtPos = new Vector3(i - this.max + this.transform.position.x, this.transform.position.y, j - this.max + this.transform.position.z);
        Vector3 spawnAtPos2 = new Vector3(i - this.max + this.transform.position.x, this.transform.position.y - 1, j - this.max + this.transform.position.z);

        if (pathLayer[i, j] == 0 && checkIfPosEmpty(spawnAtPos2))
        {
            pathLayer[i, j] = nextDistance;

            int[] next = new int[2];
            next[0] = i;
            next[1] = j;
            toDO.Enqueue(next);

            objectToBeSpawned = Instantiate(Resources.Load("Prefabs/Block"), spawnAtPos, Quaternion.identity) as GameObject;
            objectToBeSpawned.GetComponent<Renderer>().material.color = new Color(0f, (float)1 / nextDistance, 0f, 1);
            objectToBeSpawned.name = (nextDistance) + "";
        }
    }

    public bool checkIfPosEmpty(Vector3 targetPos) 
    {
        return !Physics.Raycast(targetPos, transform.up, 2);
    }
}