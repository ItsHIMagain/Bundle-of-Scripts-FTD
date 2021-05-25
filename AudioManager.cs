using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioSource laserSource;

    public AudioClip explosionSE;
    public AudioClip enemyHit;
    public AudioClip consumption;
    public AudioClip gameOver;
    public AudioClip bellow;
    public AudioClip shot;
    public AudioClip water;
    public AudioClip upgrade;
    public AudioClip laserHurt;

    public AudioClip laser;
    private GameObject[] lasers;

    private void Start()
    {
        instance = this;
    }

    public void playSound(string sound)
    {
        switch (sound)
        {
            case "explosionSE":
                audioSource.PlayOneShot(explosionSE);
                break;
            case "enemyHit":
                audioSource.PlayOneShot(enemyHit);
                break;
            case "consumption":
                audioSource.PlayOneShot(consumption);
                break;
            case "GameOver":
                audioSource.PlayOneShot(gameOver);
                break;
            case "bellow":
                audioSource.PlayOneShot(bellow);
                break;
            case "pew":
                audioSource.PlayOneShot(shot);
                break;
            case "upgrade":
                audioSource.PlayOneShot(upgrade);
                break;
            case "laserhurt":
                audioSource.PlayOneShot(laserHurt);
                break;
        }
    }

    private void Update()
    {
        lasers = GameObject.FindGameObjectsWithTag("Laser");
        if(lasers.Length != 0 &&  !laserSource.clip == laser && !laserSource.isPlaying)
        {
            laserSource.PlayOneShot(laser, 0.2f);
        } else if (lasers.Length == 0)
        {
            laserSource.Stop();
        }
    }
}
//AudioManager.instance.playSound("upgrade");
