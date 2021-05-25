using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public static cameraController instance;

    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;

    public float rotationMultiplier;

    private GameObject tentacleBase;

    private void Start()
    {
        tentacleBase = GameObject.FindGameObjectWithTag("Base");
        instance = this;
    }

    private void Update()
    {
        //Follow player
        if (transform.position.x > -1.5f && tentacleBase.transform.position.x < transform.position.x && tentacleBase.activeSelf == true)
        {
            Vector3 newPosition = new Vector3(tentacleBase.transform.position.x, 0.9f, transform.position.z);
            transform.position = Vector3.Slerp(transform.position, newPosition, 0.5f * Time.deltaTime);
        }
        else if (transform.position.x < 1.5f && tentacleBase.transform.position.x > transform.position.x && tentacleBase.activeSelf == true)
        {
            Vector3 newPosition = new Vector3(tentacleBase.transform.position.x, 0.9f, transform.position.z);
            transform.position = Vector3.Slerp(transform.position, newPosition, 0.5f * Time.deltaTime);
        }
        else if (tentacleBase.activeSelf == false)
        {
            //Center on death
            Vector3 newPosition = new Vector3(0, 0.9f, transform.position.z);
            transform.position = Vector3.Slerp(transform.position, newPosition, 2f * Time.deltaTime);
        }

        if (transform.position.y != 0.9 && tentacleBase.activeSelf == true)
        {
            //return to default
            Vector3 newPosition = new Vector3(0, 0.9f, transform.position.z);
            transform.position = Vector3.Slerp(transform.position, newPosition, 3f * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmmount = Random.Range(-1f, 1f) * shakePower;
            float yAmmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmmount, yAmmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }
        transform.rotation = Quaternion.Euler(0f,0f,shakeRotation * Random.Range(-1f, 1f));
    }

    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;

        shakeRotation = power * rotationMultiplier;
    }
}
