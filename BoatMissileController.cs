using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMissileController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float dropSpeed;
    private float rotateZ;

    private bool facingLeft;
    public GameObject target;

    public GameObject explosion;
    private GameObject hitBox;
    public GameObject bouncedMissile;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Base");
        hitBox = GameObject.FindGameObjectWithTag("Deathbox");

          if(target.transform.position.x < transform.position.x){
              rotateZ = -45;
              facingLeft = true;
              rb.AddForce(new Vector3(-40f, dropSpeed, 0));
            } else {
            rotateZ = 45;
            facingLeft = false;
            rb.AddForce(new Vector3(40f, dropSpeed, 0));
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
      }
    }

    void FixedUpdate()
    {
      //Find tentacle and set direction
          if(rotateZ < 80 && facingLeft == true){
            rotateZ += 1.8f;
            } else if(rotateZ > -80){
            rotateZ -= 1.8f;
            }

       //Drop and move
        if(dropSpeed >= 0 && facingLeft == true){
          dropSpeed-=1f;
          rb.AddForce(new Vector3(-7.5f,dropSpeed,0));

        } else if(dropSpeed >= 0 && facingLeft == false){
          dropSpeed-=1f;
          rb.AddForce(new Vector3(7.5f,dropSpeed,0));
        }
        //Rotate
        transform.eulerAngles = new Vector3 (0, 0, rotateZ);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Tentacle")
        {
            if (UpgradeManager.instance.bouncy == false)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                cameraController.instance.StartShake(.2f, .2f);
                hitBox.GetComponent<playerHitbox>().Damage(25);
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
                    cameraController.instance.StartShake(.2f, .2f);
                    hitBox.GetComponent<playerHitbox>().Damage(25);
                    AudioManager.instance.playSound("explosionSE");
                    Destroy(gameObject);
                }
            }
        }

        if (other.gameObject.tag == "Deathbox")
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
