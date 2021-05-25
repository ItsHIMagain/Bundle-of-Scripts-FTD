using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserController : MonoBehaviour
{
    private GameObject hitBox;
    //public GameObject laserSplash;
    private float timer = 8;

    void Start()
    {
        hitBox = GameObject.FindGameObjectWithTag("Deathbox");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tentacle")
        {
            cameraController.instance.StartShake(.1f, .1f);
            hitBox.GetComponent<playerHitbox>().Damage(1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Splash")
        {
            GameObject.FindGameObjectWithTag("Splash").GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void Update()
    {
        timer -= 1 * Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
