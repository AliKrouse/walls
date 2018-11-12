using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textController : MonoBehaviour
{
    public Text t;

    public aiSpawner spawner;
    public makeWall builder;
    public GameObject bricksUI, scoreUI;

    public ai firstAi, secondAi, thirdAi;
    private GameObject thirdAiObject;

    private Coroutine openingText, onDestroyText, endingText;

    public GameObject cover, titleWalls, titleTrust;
    private Image coverImage;
    private Text wallsText, trustText;

    private bool gameOver, holdBeforeWalls, holdBeforeFade;

    private float aText, aC, aW, aT;

    private KeyCode[] numberKeys =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4
    };
    
	void Start ()
    {
        t.text = "";
        openingText = StartCoroutine(runBeginningText());

        thirdAiObject = thirdAi.gameObject;

        coverImage = cover.GetComponent<Image>();
        wallsText = titleWalls.GetComponent<Text>();
        trustText = titleTrust.GetComponent<Text>();

        bricksUI.SetActive(false);
        scoreUI.SetActive(false);
	}
	
	void Update ()
    {
        if (thirdAiObject == null && onDestroyText == null)
        {
            StopCoroutine(openingText);
            onDestroyText = StartCoroutine(runDestroyText());
        }
        if (thirdAiObject == null && heart.hp >= 10 && endingText == null)
            endingText = StartCoroutine(runEndText());

        if (gameOver == true && t.color.a > 0)
        {
            aText = t.color.a;
            aText -= Time.deltaTime;
            t.color = new Color(1, 1, 1, aText);
        }
        if (t.color.a <= 0.75 && coverImage.color.a < 1)
        {
            cover.SetActive(true);
            aC = coverImage.color.a;
            aC += Time.deltaTime;
            coverImage.color = new Color(0, 0, 0, aC);
        }
        if (coverImage.color.a >= 1 && !holdBeforeWalls)
        {
            StartCoroutine(hold1());
        }
        if (holdBeforeWalls && !holdBeforeFade)
        {
            titleWalls.SetActive(true);
            //titleTrust.SetActive(true);
            //trustText.color = new Color(1, 1, 1, 0);
            //StartCoroutine(hold2());
        }
        //if (holdBeforeFade)
        //{
        //    aW = wallsText.color.a;
        //    aW -= Time.deltaTime;
        //    wallsText.color = new Color(1, 1, 1, aW);

        //    aT = trustText.color.a;
        //    aT += Time.deltaTime;
        //    trustText.color = new Color(1, 1, 1, aT);
        //}

        for (int i = 0; i < numberKeys.Length; i++)
        {
            if (Input.GetKeyDown(numberKeys[i]))
                Time.timeScale = i + 1;
        }
	}

    private IEnumerator runBeginningText()
    {
        yield return new WaitForSeconds(1f);
        firstAi.speed = 1.5f;
        yield return new WaitForSeconds(0.5f);
        t.text = "Check it out, we have a friend coming in!";
        yield return new WaitForSeconds(3f);
        t.text = "";
        yield return new WaitForSeconds(4f);
        t.text = "Well shit.";
        yield return new WaitForSeconds(3f);
        t.text = "That... that hurt.";
        yield return new WaitForSeconds(3f);
        t.text = "";
        secondAi.speed = 1.5f;
        yield return new WaitForSeconds(1f);
        t.text = "Oh good, another friend is coming.";
        yield return new WaitForSeconds(2f);
        t.text = "Maybe they can make it better.";
        yield return new WaitForSeconds(2f);
        t.text = "";
        yield return new WaitForSeconds(2f);
        t.text = "God damnit.";
        yield return new WaitForSeconds(3f);
        t.text = "";
        thirdAi.speed = 0.5f;
        yield return new WaitForSeconds(1f);
        t.text = "Someone else is coming, but there's no way I'm getting hurt again.";
        yield return new WaitForSeconds(2f);
        t.text = "You have to stop them, keep me safe.";
        yield return new WaitForSeconds(2f);
        t.text = "Click and drag with your mouse to build walls around me.";
        builder.bricks = builder.maxBricks;
        foreach (GameObject g in builder.brickIcons)
            g.SetActive(true);
        bricksUI.SetActive(true);
        scoreUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        t.text = "";
    }

    private IEnumerator runDestroyText()
    {
        if (heart.hp > 5)
        {
            t.text = "";
            yield return new WaitForSeconds(1f);
            t.text = "That still hurt a little but... it wasn't as bad.";
            yield return new WaitForSeconds(3f);
            t.text = "Don't let them get to me again, okay?";
            yield return new WaitForSeconds(3f);
            t.text = "I don't want to get hurt again.";
            yield return new WaitForSeconds(3f);
            t.text = "";
        }
        if (heart.hp < 5)
        {
            t.text = "OW, FUCKIN HELL.";
            yield return new WaitForSeconds(3f);
            t.text = "Yea, fuck this. Do NOT let them get close to me anymore.";
            yield return new WaitForSeconds(3f);
            t.text = "";
        }
        spawner.StartCoroutine(spawner.spawnAI());
    }

    private IEnumerator runEndText()
    {
        t.text = "";
        yield return new WaitForSeconds(1f);
        t.text = "...";
        yield return new WaitForSeconds(3f);
        t.text = "I thought they would hurt me.";
        yield return new WaitForSeconds(3f);
        t.text = "I thought I had to keep them away to protect myself.";
        yield return new WaitForSeconds(3f);
        t.text = "...";
        yield return new WaitForSeconds(3f);
        t.text = "I'm so sorry. I should have trusted you.";
        yield return new WaitForSeconds(3f);
        t.text = "I just didn't want to get hurt anymore.";
        yield return new WaitForSeconds(3f);
        t.text = "Thank you.";
        yield return new WaitForSeconds(3f);
        gameOver = true;
        spawner.StopAllCoroutines();
    }

    private IEnumerator hold1()
    {
        yield return new WaitForSeconds(3f);
        holdBeforeWalls = true;
    }

    private IEnumerator hold2()
    {
        yield return new WaitForSeconds(3f);
        holdBeforeFade = true;
    }
}
