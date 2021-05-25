using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float distance;

    private bool destroy;
    public GameObject waterDrop;

    private bool collidingEnemy = false;
    //Shooting stuff
    public float shotDelayStart;
    private float shotDelay;
    public GameObject boatMissile;
    public Transform shotPosition;
    public Animator anim;
    public GameObject explosion;
    public GameObject explosionMed;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Base");
        shotDelay = shotDelayStart;
    }

    void FixedUpdate()
    {
        //Move
        if(Vector3.Distance(target.transform.position, transform.position) > distance && collidingEnemy == false){
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, -3.2f, transform.position.z), speed * Time.deltaTime);
        } else if(collidingEnemy != false || (Vector3.Distance(target.transform.position, transform.position) <= distance)){
        //Shoot
            shotDelay-=1;
            if(shotDelay <= 0 && destroy == false){
                AudioManager.instance.playSound("pew");
                Instantiate(boatMissile, shotPosition.position, Quaternion.identity);
                shotDelay = shotDelayStart;
            }
        }
    }

          void OnBecameInvisible() {
         if (destroy == true){
             Destroy(gameObject);
             ScoreManager.score += 100;
            }
          }

        void OnCollisionEnter2D(Collision2D other) {
              if (other.gameObject.tag == "Tentacle" && destroy == false)
              {
                    cameraController.instance.StartShake(.1f, .1f);
                    anim.SetBool("Damaged", true);
                    Instantiate(waterDrop, transform.position, Quaternion.identity);
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    AudioManager.instance.playSound("explosionSE");
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                    gameObject.GetComponent<Bobbing>().enabled = false;
            destroy = true;
              }

            if (other.gameObject.tag == "Enemy" && gameObject.GetComponent<Renderer>().isVisible != true)
            {
            Destroy(gameObject);
            } else if (other.gameObject.tag == "Enemy")
            {
            collidingEnemy = true;
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Deathbox")
                {
                cameraController.instance.StartShake(.1f, .1f);
                ScoreManager.score += 100;
                Destroy(gameObject);
                }

        if (other.gameObject.tag == "Laser")
        {
            anim.SetBool("Damaged", true);
            Instantiate(explosionMed, transform.position, Quaternion.identity);
            destroy = true;
            AudioManager.instance.playSound("explosionSE");
            Destroy(gameObject, 0.2f);
        }
    }

        void OnCollisionExit2D(Collision2D other) {
                    if (collidingEnemy == true){
                        collidingEnemy = false;
                    }
        }
}