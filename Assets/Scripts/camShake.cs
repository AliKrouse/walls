using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour
{
    public static float shakeTimer;

    public float shakeDistance;

    private Vector3 center;
    
	void Start ()
    {
        center = transform.position;
	}
	
	void Update ()
    {
        if (shakeTimer > 0)
        {
            transform.position = center + Random.insideUnitSphere * shakeDistance;

            shakeTimer -= Time.deltaTime;
        }
        else
            transform.position = center;
	}
}
