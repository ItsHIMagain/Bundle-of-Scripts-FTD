using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicopterMissile : MonoBehaviour
{
    public Rigidbody2D rb;
    private GameObject tentacleBase;
    public GameObject explosion;
    private float speed = 2.2f;
    private bool dir = false;

    private GameObject hitBox;
    public GameObject bouncedMissile;
    void FixedUpdate()
    {
        if (dir == false)
        {
            rb.AddForce(new Vector3(-speed, 0, 0));
        }
        else
        {
            rb.AddForce(new Vector3(speed, 0, 0));

        }
        speed += 0.6f;
    }

    void Start()
    {
        tentacleBase = GameObject.FindGameObjectWithTag("Base");
        hitBox = GameObject.FindGameObjectWithTag("Deathbox");
        if (gameObject.transform.position.x <= tentacleBase.transform.position.x)
        {
            dir = true;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tentacle")
        {
            if (UpgradeManager.instance.bouncy == false)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                hitBox.GetComponent<playerHitbox>().Damage(35);
                cameraController.instance.StartShake(.2f, .1f);
                AudioManager.instance.playSound("explosionSE");
                Destroy(gameObject);
            }
            else
            {
                int rng = Random.Range(0, 5);
                if (rng == 1)
                {
                    Instantiate(bouncedMissile, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else
                {
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    hitBox.GetComponent<playerHitbox>().Damage(35);
                    cameraController.instance.StartShake(.2f, .1f);
                    AudioManager.instance.playSound("explosionSE");
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
