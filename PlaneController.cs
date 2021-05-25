using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed;
     private Vector3 target;
     private Vector3 altTarget;
     public GameObject tentacleBase;
     
     private bool destroy = false;

     public GameObject missileSpawn;
     public GameObject missile;
     private bool hasShot = false;

    public GameObject waterDrop;

    public GameObject explosion;
    public GameObject explosionMed;


    private float angle = 0;
    public float distance;

    public bool gravity = false;

    public float HP;



    public Animator anim;

    void Start(){
        tentacleBase = GameObject.FindGameObjectWithTag("Base");
    }

     void Update() {

                target = tentacleBase.transform.position; // Tentacle base's position
                altTarget = new Vector3(target.x, target.y+5f, target.z); //Tentacle Base position with modifiers
              //Movement
              if(Vector3.Distance(altTarget, transform.position) > distance && gravity == false && anim.GetBool("BombDropped") == false){
                transform.position = Vector3.MoveTowards(transform.position, altTarget, speed * Time.deltaTime);
              } else if (gravity == false) {
                //Flying up
                speed+=0.06f;
                destroy = true;
                
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(altTarget.x, transform.position.y +1, transform.position.z), speed * Time.deltaTime);
                if(hasShot==false && Vector3.Distance(altTarget, transform.position) < distance){
                anim.SetBool("BombDropped", true);
                AudioManager.instance.playSound("pew");
                Instantiate(missile, missileSpawn.transform.position, Quaternion.identity);
                hasShot = true;
                }
              }

                //Rotate
                Vector3 vectorToTarget = altTarget - transform.position;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
     }

      void OnBecameInvisible() {
         if (destroy == true){
             Destroy(gameObject);
         }
     }

         void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Water")
        {
            Instantiate(waterDrop, transform.position, Quaternion.identity);
            ScoreManager.score += 100;
            destroy = true;
        }

        if (other.gameObject.tag == "Deathbox")
        {
            cameraController.instance.StartShake(.1f, .1f);
            ScoreManager.score += 100;
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Laser")
        {
            AudioManager.instance.playSound("ExplosionSE");
            HP = 0;
            AudioManager.instance.playSound("explosionSE");
            anim.SetBool("Damaged", true);
            Instantiate(explosionMed, transform.position, Quaternion.identity);
            destroy = true;
            Destroy(gameObject, 0.3f);
        }

    }

      void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.tag == "Tentacle" && HP <= 0 && gravity == false)
            {
            Instantiate(explosion, transform.position, Quaternion.identity);
            gravity = true;
            anim.SetBool("Damaged", true);
            GetComponent<Rigidbody2D>().gravityScale = 2;

            if (this.enabled)
            {
                this.enabled = false;
                AudioManager.instance.playSound("ExplosionSE");
            }
      } else if (gravity == false && other.gameObject.tag == "Tentacle")
            cameraController.instance.StartShake(.1f, .1f);
            HP -= StatManager.dmg;
            if (HP > 0)
            {
            AudioManager.instance.playSound("enemyHit");
            }
    }
}
