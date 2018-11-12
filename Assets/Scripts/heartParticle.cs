using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartParticle : MonoBehaviour
{
    private SpriteRenderer sr;
    private float a, scaleX, scaleY;
    public float multiplier;
    
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        transform.Translate(transform.up * Time.deltaTime);

        a = sr.color.a;
        a -= Time.deltaTime;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);

        if (a <= 0)
            Destroy(this.gameObject);

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleX += Time.deltaTime * multiplier;
        scaleY += Time.deltaTime * multiplier;
        transform.localScale = new Vector3(scaleX, scaleY, 1);
	}
}
