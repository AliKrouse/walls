using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiSpawner : MonoBehaviour
{
    public float X, yMax, yMin;

    private PolygonCollider2D col;
    private Vector2 point;

    public GameObject[] aiUnits;

    public float waitTime, subtraction, minimumWaitTime;
    private bool pointIsSet;
    
	void Start ()
    {
        col = GetComponent<PolygonCollider2D>();
        //StartCoroutine(spawnAI());
	}

    public IEnumerator spawnAI()
    {
        while (true)
        {
            while (!pointIsSet)
            {
                point = new Vector2(Random.Range(-X, X), Random.Range(yMin, yMax));
                if (col.OverlapPoint(point))
                    pointIsSet = true;
            }
            int aiSelection = Random.Range(0, aiUnits.Length);
            Instantiate(aiUnits[aiSelection], point, Quaternion.identity);
            if (waitTime > minimumWaitTime)
                waitTime -= subtraction;
            if (waitTime < minimumWaitTime)
                waitTime = minimumWaitTime;
            pointIsSet = false;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
