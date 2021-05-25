using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLaser : MonoBehaviour
{
    public float timer = 5;
    public GameObject laser;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        timer -= 1 * Time.deltaTime;

        if(timer <= 0)
        {
            Instantiate(laser, transform.position, Quaternion.Euler(0f,0,90f));
            Destroy(gameObject);
        }
    }
}
