using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heart : MonoBehaviour
{
    public float maxHp;
    public static float hp;
    public Scrollbar bar;

    public makeWall builder;
    public aiSpawner spawner;
    public textController text;
    
	void Start ()
    {
        hp = maxHp;
	}
	
	void Update ()
    {
        bar.size = hp / maxHp;

        if (hp <= 0)
        {
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject g in walls)
                Destroy(g);
            builder.bricks = 0;
            foreach (GameObject g in builder.brickIcons)
                g.SetActive(false);
        }
	}
}
