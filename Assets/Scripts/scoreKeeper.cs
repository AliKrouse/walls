using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreKeeper : MonoBehaviour
{
    public static int score;
    private Text t;
    
	void Start ()
    {
        t = GetComponent<Text>();
	}
	
	void Update ()
    {
        t.text = score.ToString();
	}
}
