using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public float hp;

    private Coroutine destroyWall;

    public bool isActive;

    private void Start()
    {
        isActive = true;
    }

    void Update ()
    {
        if (hp <= 0 && destroyWall == null)
        {
            destroyWall = StartCoroutine(waitAndDestroy());
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AI"))
        {
            if (collision.gameObject.GetComponent<ai>().AITYPE == "heavy" && destroyWall == null)
            {
                destroyWall = StartCoroutine(waitAndDestroy());
                Destroy(collision.gameObject);
            }
            else
                hp--;
        }
    }

    private IEnumerator waitAndDestroy()
    {
        isActive = false;
        transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(10f);
        Destroy(transform.parent.gameObject);
    }
}
