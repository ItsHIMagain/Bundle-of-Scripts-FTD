using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    private GameObject target;
    public float speed;
    public float distance;

    private bool destroy;
    public GameObject waterDrop;

    //Shooting stuff
    public float shotDelayStart;
    private float shotDelay;
    public GameObject missile;
    public Transform shotPosition;
    public Animator anim;

    //Death
    public GameObject explosion;
    public GameObject explosionMed;
    public Rigidbody2D rb;
    public float hp;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Base");
        shotDelay = shotDelayStart;
    }

    private void FixedUpdate()
    {
        //Move
        if (Vector3.Distance(target.transform.position, transform.position) > distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, 1f, transform.position.z), speed * Time.deltaTime);
        }
        else
        {
            //Shoot
            shotDelay -= 1;
            if (shotDelay <= 0 && destroy == false && (gameObject.transform.rotation.eulerAngles.z >= -3 && gameObject.transform.rotation.eulerAngles.z <= 3))
            {
                Instantiate(missile, shotPosition.position, Quaternion.identity);
                AudioManager.instance.playSound("pew");
                shotDelay = shotDelayStart;
            }
        }

        //Rotate
        Vector3 targetVector = target.transform.position; // Tentacle base's position
        Vector3 altTarget = new Vector3(targetVector.x, targetVector.y + 5f, targetVector.z); //Tentacle Base position with modifiers
        Vector3 vectorToTarget = altTarget - transform.position;
        Quaternion q = Quaternion.AngleAxis(0, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 3.5f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Tentacle" && destroy == false && hp <= 0)
        {
            cameraController.instance.StartShake(.1f, .1f);
            anim.SetBool("Damaged", true);
            Instantiate(waterDrop, transform.position, Quaternion.identity);
            Instantiate(explosion, transform.position, Quaternion.identity);
            AudioManager.instance.playSound("ExplosionSE");
            destroy = true;
            rb.gravityScale = 1.5f;
        }
        else if (other.gameObject.tag == "Tentacle")
        {
            cameraController.instance.StartShake(.1f, .1f);
            hp -= StatManager.dmg;
            if (hp > 0)
            {
                AudioManager.instance.playSound("enemyHit");
            }
        }

        if (other.gameObject.tag == "Enemy")
        {
            cameraController.instance.StartShake(.1f, .1f);
            anim.SetBool("Damaged", true);
            Instantiate(waterDrop, transform.position, Quaternion.identity);
            Instantiate(explosion, transform.position, Quaternion.identity);
            AudioManager.instance.playSound("ExplosionSE");
            destroy = true;
            rb.gravityScale = 1.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Deathbox")
        {
            cameraController.instance.StartShake(.1f, .1f);
            ScoreManager.score += 100;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Laser")
        {
            AudioManager.instance.playSound("explosionSE");
            anim.SetBool("Damaged", true);
            Instantiate(explosionMed, transform.position, Quaternion.identity);
            destroy = true;
            Destroy(gameObject, 0.4f);
        }
    }
}
