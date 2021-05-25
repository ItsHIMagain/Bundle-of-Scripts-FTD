using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject tentacleBase;
    public GameObject explosion;

    public GameObject bouncedMissile;
    private GameObject hitBox;
    void FixedUpdate() {
        if (gameObject.transform.position.x >= tentacleBase.transform.position.x){
            rb.AddForce(new Vector3(-8f,-0.1f,0));
        } else {
            rb.AddForce(new Vector3(8f,-0.1f,0));
            
        }
    }

    void Start() {
        tentacleBase = GameObject.FindGameObjectWithTag("Base");
        hitBox = GameObject.FindGameObjectWithTag("Deathbox");
        if(gameObject.transform.position.x <= tentacleBase.transform.position.x){
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

        void OnTriggerEnter2D(Collider2D other) {
              if (other.gameObject.tag == "Tentacle")
              {
                if(UpgradeManager.instance.bouncy == false)
                {
                Instantiate(explosion, transform.position, Quaternion.identity);
                hitBox.GetComponent<playerHitbox>().Damage(25);
                cameraController.instance.StartShake(.2f, .2f);
                AudioManager.instance.playSound("explosionSE");
                Destroy(gameObject);

            } else
                {
                    int rng = Random.Range(0,5);
                    if (rng == 1)
                    {
                    Instantiate(bouncedMissile, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                } else
                    {
                        Instantiate(explosion, transform.position, Quaternion.identity);
                        hitBox.GetComponent<playerHitbox>().Damage(25);
                        cameraController.instance.StartShake(.2f, .2f);
                        AudioManager.instance.playSound("explosionSE");
                        Destroy(gameObject);
                    }
                }
              }
              if(other.gameObject.tag == "Deathbox")
              {
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    hitBox.GetComponent<playerHitbox>().Damage(50);
                    cameraController.instance.StartShake(.6f, .2f);
                    AudioManager.instance.playSound("explosionSE");
                    Destroy(gameObject);
              }
    }

        void OnBecameInvisible() {
        Destroy(gameObject);
    }

    
}
