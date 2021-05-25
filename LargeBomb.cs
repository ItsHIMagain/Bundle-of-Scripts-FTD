using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBomb : MonoBehaviour
{
    public Rigidbody2D rb;
    private GameObject tentacleBase;
    public GameObject explosion;

    private GameObject hitBox;
    public GameObject bouncedMissile;
    private int rng = 0;
    void FixedUpdate()
    {
        if (gameObject.transform.position.x >= tentacleBase.transform.position.x)
        {
            rb.AddForce(new Vector3(-5f, -0.1f, 0));
        }
        else
        {
            rb.AddForce(new Vector3(5f, -0.1f, 0));

        }
    }

    void Start()
    {
        tentacleBase = GameObject.FindGameObjectWithTag("Base");
        hitBox = GameObject.FindGameObjectWithTag("Deathbox");
        if (gameObject.transform.position.x <= tentacleBase.transform.position.x)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    void OnBecameInvisible()
    {
        if(rng != 1)
        {
            cameraController.instance.StartShake(.3f, .2f);
            Instantiate(explosion, transform.position, Quaternion.identity);
            AudioManager.instance.playSound("explosionSE");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tentacle")
        {
            if (UpgradeManager.instance.bouncy == false)
            {
                cameraController.instance.StartShake(.4f, .2f);
                Instantiate(explosion, transform.position, Quaternion.identity);
                AudioManager.instance.playSound("explosionSE");
                Destroy(gameObject);
            }
            else
            {
                rng = Random.Range(0, 5);
                if (rng == 1)
                {
                    Instantiate(bouncedMissile, transform.position, Quaternion.identity);
                    cameraController.instance.StartShake(.2f, .2f);
                    Destroy(gameObject);
                }
                else
                {
                    
                    Destroy(gameObject);
                }
            }
        }
    }

}
