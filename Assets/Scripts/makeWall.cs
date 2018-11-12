using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeWall : MonoBehaviour
{
    public GameObject wall;
    private GameObject thisWall, wallObject;
    private SpriteRenderer wallSR;
    private BoxCollider2D wallCol;

    private Vector2 mousePos;

    public int bricks;
    public int maxBricks;
    public GameObject[] brickIcons;
    public float waitTime;

    private Coroutine brickRecharge;
    
	void Start ()
    {
        //bricks = maxBricks;
        foreach (GameObject g in brickIcons)
            g.SetActive(false);
	}
	
	void Update ()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (bricks > 0)
            {
                thisWall = Instantiate(wall, mousePos, Quaternion.identity);
                wallObject = thisWall.transform.GetChild(0).gameObject;
                wallSR = wallObject.GetComponent<SpriteRenderer>();
                wallCol = wallObject.GetComponent<BoxCollider2D>();
                bricks--;
                brickIcons[bricks].SetActive(false);

                if (brickRecharge != null)
                    StopCoroutine(brickRecharge);
                brickRecharge = StartCoroutine(getBricks());
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (thisWall != null)
            {
                thisWall.transform.LookAt(mousePos);

                Vector2 midpoint = (Vector2)thisWall.transform.position + (mousePos - (Vector2)thisWall.transform.position) / 2;
                float distance = Vector2.Distance(thisWall.transform.position, mousePos);
                wallObject.transform.position = midpoint;
                wallSR.size = new Vector2(distance, 0.5f);
                wallCol.size = new Vector2(distance, 0.5f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (thisWall != null)
            {
                wallCol.enabled = true;
                thisWall = null;
            }
        }
	}

    private IEnumerator getBricks()
    {
        yield return new WaitForSeconds(waitTime + bricks);
        brickIcons[bricks].SetActive(true);
        bricks++;

        if (bricks < maxBricks)
            brickRecharge = StartCoroutine(getBricks());
    }
}
