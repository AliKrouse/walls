using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai : MonoBehaviour
{
    public string AITYPE;
    public GameObject target;
    public float distance, speed;

    public float maxDist;

    public float multiplier;

    private Coroutine spawnHearts;
    public GameObject heartParticle;
    public float heartSpawnTime;
    
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Heart");
	}
	
	void Update ()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);

        distance = Vector2.Distance(transform.position, target.transform.position);

        float angle = Mathf.Atan2((transform.position.y - target.transform.position.y), (transform.position.x - target.transform.position.x)) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis((angle + 180), Vector3.forward);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            if (AITYPE == "first")
            {
                heart.hp -= 2f;
                Destroy(this.gameObject);
                camShake.shakeTimer = 0.5f;
            }
            else
            {
                heart.hp += 10;
                if (spawnHearts == null)
                    spawnHearts = StartCoroutine(heartSpawner());
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.GetChild(0).gameObject.GetComponent<wall>().isActive)
            {
                float damage = (2.5f / distance) * multiplier;
                heart.hp -= damage;
                scoreKeeper.score++;
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator heartSpawner()
    {
        while (true)
        {
            Instantiate(heartParticle, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(heartSpawnTime);
        }
    }
}
