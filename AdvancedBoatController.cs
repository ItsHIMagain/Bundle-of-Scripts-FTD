using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedBoatController : MonoBehaviour
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
    public Transform shotPosition2;
    public Transform shotPosition3;
    public Animator anim;
    public GameObject explosion;
    public GameObject explosionMed;

    void Start()
    {
        shotDelay = shotDelayStart;
        target = GameObject.FindGameObjectWithTag("Base");

    }

    void FixedUpdate()
    {
        //Move
        if(Vector3.Distance(target.transform.position, transform.position) > distance && collidingEnemy == false){
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, -3.2f, transform.position.z), speed * Time.deltaTime);
        } else{
        //Shoot
            shotDelay-=1;
            if(shotDelay <= 0 && destroy == false){
                shotDelay = shotDelayStart;
                StartCoroutine(shoot());
            }
        }
    }

        private IEnumerator shoot(){
        Instantiate(boatMissile, shotPosition.position, Quaternion.identity);
        AudioManager.instance.playSound("pew");
        yield return new WaitForSeconds(0.07f);
        Instantiate(boatMissile, shotPosition2.position, Quaternion.identity);
        AudioManager.instance.playSound("pew");
        yield return new WaitForSeconds(0.07f);
        Instantiate(boatMissile, shotPosition3.position, Quaternion.identity);
        AudioManager.instance.playSound("pew");
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
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    anim.SetBool("Damaged", true);
                    cameraController.instance.StartShake(.1f, .1f);
                    Instantiate(waterDrop, transform.position, Quaternion.identity);
                    destroy = true;
                    AudioManager.instance.playSound("explosionSE");
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                    gameObject.GetComponent<Bobbing>().enabled = false;
        }

        if (other.gameObject.tag == "Enemy" && gameObject.GetComponent<Renderer>().isVisible != true)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            collidingEnemy = true;
        }
    }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Deathbox")
                {
                cameraController.instance.StartShake(.1f, .1f);
                ScoreManager.score += 200;
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
