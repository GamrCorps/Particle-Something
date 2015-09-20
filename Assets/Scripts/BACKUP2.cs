/*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BetterPathFinder : MonoBehaviour
{
    public int maxPathLength = 20;
    public int[,] grid;
    public Queue<int[]> queue = new Queue<int[]>(), queue2 = new Queue<int[]>();
    private Vector3 pos;
    private int stage = 0;
    public GameObject toFind;
    public bool showGrid = false;
    public bool showPath = true;
    public List<GameObject> objectsOnTheGrid;

    void Start()
    {
        createPathfinder();
    }

    void Update()
    {
    }

    void NextBlock(int[] coords)
    {
        if (coords[0] <= 0 || 0 >= coords[1] || coords[0] >= maxPathLength * 2 || maxPathLength * 2 <= coords[1]) return;
        if (toFind.transform.position.x == coords[0] - maxPathLength + pos.x && toFind.transform.position.z == coords[1] - maxPathLength + pos.z)
        {
            stage = 1;
            queue.Clear();
            queue2.Enqueue(coords);
            if (showPath)
            {
                GameObject obj = Instantiate(Resources.Load("Prefabs/Block"), new Vector3(coords[0] - maxPathLength + pos.x, pos.y - (((float)coords[2] + 1f) / ((float)maxPathLength * 3f)) + 1, coords[1] - maxPathLength + pos.z), Quaternion.identity) as GameObject;
                //-----
                objectsOnTheGrid.Add(obj);
                //-----
            }
        }
        else if (grid[coords[0], coords[1]] == 0 && !Physics.Raycast(new Vector3(coords[0] - maxPathLength + pos.x, pos.y - 1, coords[1] - maxPathLength + pos.z), transform.up, 2))
        {
            grid[coords[0], coords[1]] = coords[2];
            queue.Enqueue(new int[] { coords[0], coords[1] });
            if (showGrid)
            {
                GameObject obj = Instantiate(Resources.Load("Prefabs/Block"), new Vector3(coords[0] - maxPathLength + pos.x, pos.y - (((float)coords[2] + 1f) / ((float)maxPathLength * 3f)), coords[1] - maxPathLength + pos.z), Quaternion.identity) as GameObject;
                //-----
                objectsOnTheGrid.Add(obj);
                //-----
                float col = (((float)coords[2] + 1f) / ((float)maxPathLength * 3f));
                obj.GetComponent<Renderer>().material.color = new Color(col, col, col, 1);
                obj.name = (coords[2]) + "";
            }
        }
    }
    bool TestBlock(int[] coords)
    {
        if (coords[0] <= 0 || 0 >= coords[1] || coords[0] >= maxPathLength * 2 || maxPathLength * 2 <= coords[1])
            return false;
        if (grid[coords[0], coords[1]] == coords[2])
        {
            return true;
        }

        return false;
    }

    public void createPathfinder()
    {
        //-----
        foreach (GameObject obj in objectsOnTheGrid)
        {
            Destroy(obj);
        }

        stage = 0;
        queue = new Queue<int[]>();
        queue2 = new Queue<int[]>();

        //-----

        objectsOnTheGrid = new List<GameObject>();
        pos = this.transform.position;
        queue.Enqueue(new int[] { maxPathLength, maxPathLength });
        grid = new int[maxPathLength * 2, maxPathLength * 2];
        grid[maxPathLength, maxPathLength] = 0;
        while (stage == 0)
        {
            if (queue.Count == 0)
                break;
            int[] next = (int[])queue.Dequeue();
            int nextDistance = grid[next[0], next[1]] + 1;
            NextBlock(new int[] { next[0] + 1, next[1], nextDistance });
            NextBlock(new int[] { next[0] - 1, next[1], nextDistance });
            NextBlock(new int[] { next[0], next[1] + 1, nextDistance });
            NextBlock(new int[] { next[0], next[1] - 1, nextDistance });
        }
        if (showPath)
        {
            while (stage == 1)
            {
                if (queue2.Count == 0)
                    break;
                int[] next = (int[])queue2.Dequeue();
                int nextDistance = next[2] - 1;
                List<int[]> temp = new List<int[]>();
                if (TestBlock(new int[] { next[0] + 1, next[1], nextDistance })) { temp.Add(new int[] { next[0] + 1, next[1], nextDistance }); }
                if (TestBlock(new int[] { next[0] - 1, next[1], nextDistance })) { temp.Add(new int[] { next[0] - 1, next[1], nextDistance }); }
                if (TestBlock(new int[] { next[0], next[1] + 1, nextDistance })) { temp.Add(new int[] { next[0], next[1] + 1, nextDistance }); }
                if (TestBlock(new int[] { next[0], next[1] - 1, nextDistance })) { temp.Add(new int[] { next[0], next[1] - 1, nextDistance }); }
                if (temp.Count == 0)
                    continue;
                if (temp.Count == 1)
                {
                    queue2.Enqueue(temp.ToArray()[0]);
                    int[] coords = temp.ToArray()[0];
                    if (showPath)
                    {
                        GameObject obj = Instantiate(Resources.Load("Prefabs/Block"), new Vector3(coords[0] - maxPathLength + pos.x, pos.y - (((float)coords[2] + 1f) / ((float)maxPathLength * 3f)) + 1, coords[1] - maxPathLength + pos.z), Quaternion.identity) as GameObject;
                        //-----
                        objectsOnTheGrid.Add(obj);
                        //-----
                    }
                }
                if (temp.Count >= 2)
                {
                    int[] coords = temp.ToArray()[(int)Mathf.Floor(Random.Range(0, temp.Count))];
                    queue2.Enqueue(coords);
                    if (showPath)
                    {
                        GameObject obj = Instantiate(Resources.Load("Prefabs/Block"), new Vector3(coords[0] - maxPathLength + pos.x, pos.y - (((float)coords[2] + 1f) / ((float)maxPathLength * 3f)) + 1, coords[1] - maxPathLength + pos.z), Quaternion.identity) as GameObject;
                        //-----
                        objectsOnTheGrid.Add(obj);
                        //-----
                    }
                }
            }
        }

    }
}
*/