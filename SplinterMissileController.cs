using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplinterMissileController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject tentacleBase;
    public GameObject explosion;
    public GameObject missile;

    private GameObject hitBox;
    public float lifetime;

        private void FixedUpdate()
    {
        lifetime -= 1 * Time.deltaTime;
        if (gameObject.transform.position.x >= tentacleBase.transform.position.x)
        {
            rb.AddForce(new Vector3(-8, -0.1f, 0));
        }
        else
        {
            rb.AddForce(new Vector3(8, -0.1f, 0));
        }
    }

    void Update() {
        
        if(lifetime <= 0){
            if (gameObject.transform.position.x <= tentacleBase.transform.position.x)
            {
                cameraController.instance.StartShake(.1f, .1f);
                Instantiate(explosion, transform.position, Quaternion.identity);
                Instantiate(missile, transform.position, Quaternion.Euler(new Vector3(0, 0, -45)));
                Instantiate(missile, transform.position + new Vector3(0.4f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -45)));
                Instantiate(missile, transform.position + new Vector3(-0.4f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -45)));
                AudioManager.instance.playSound("explosionSE");
                Destroy(gameObject);
            } else
            {
                cameraController.instance.StartShake(.1f, .1f);
                Instantiate(explosion, transform.position, Quaternion.identity);
                Instantiate(missile, transform.position, Quaternion.Euler(new Vector3(0, 0, 45)));
                Instantiate(missile, transform.position + new Vector3(0.4f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 45)));
                Instantiate(missile, transform.position + new Vector3(-0.4f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 45)));
                AudioManager.instance.playSound("explosionSE");
                Destroy(gameObject);
            }
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
            Instantiate(explosion, transform.position, Quaternion.identity);
            cameraController.instance.StartShake(.1f, .1f);
            hitBox.GetComponent<playerHitbox>().Damage(15);
            AudioManager.instance.playSound("explosionSE");
            Destroy(gameObject);
              }
    }

        void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
