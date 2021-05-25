using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHitbox : MonoBehaviour
{
    public float defaultDmg;

    public Transform smokeSpawnL;
    public Transform smokeSpawnR;
    public GameObject smoke;
    public GameObject health;
    public GameObject explosion;

    public bool improvedRegen = false;

    public bool screech = false;
    public GameObject screechEffect;
    public float screechTimer;

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Enemy")
        {
            if (StatManager.hp < StatManager.maxHP){
                if(improvedRegen == false)
                {
                    StatManager.hp += 25;
                    Instantiate(explosion, transform.position, Quaternion.identity);
                }
                else if (improvedRegen == true)
                {
                    StatManager.hp += 50;
                    Instantiate(explosion, transform.position, Quaternion.identity);
                }
                Instantiate(health, new Vector3(smokeSpawnL.position.x-0.6f, smokeSpawnL.position.y, smokeSpawnL.position.z), Quaternion.identity);
                Instantiate(health, new Vector3(smokeSpawnR.position.x+0.6f, smokeSpawnR.position.y, smokeSpawnR.position.z), Quaternion.identity);
                if (StatManager.hp >= StatManager.maxHP)
                {
                    StatManager.hp = StatManager.maxHP;
                }
            }
            AudioManager.instance.playSound("consumption");
        }
    }
        public void Damage(float damage)
        {
        if (screech == true && StatManager.hp <= 200 && screechTimer <= 0)
        {
            AudioManager.instance.playSound("bellow");
            Instantiate(screechEffect, transform.position, Quaternion.identity);
            screechTimer = 20;
        }

        StatManager.hp -= damage;
        Instantiate(smoke, smokeSpawnL.position, Quaternion.identity);
        Instantiate(smoke, smokeSpawnR.position, Quaternion.identity);
    }

    private void Update()
    {
        if (screech == true && screechTimer > 0)
        {
            screechTimer -= 1 * Time.deltaTime;
        }
    }
}
