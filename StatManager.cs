using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static float hp;
    public static float dmg;
    public static bool alive = true;

    public float StartHP;
    static public float maxHP;
    public float startDmg;
    public Animator saveScoreButton;
    public Animator resetMenu;
    public GameObject spawner;

    void Start() {
        hp = StartHP;
        dmg = startDmg;
        maxHP = StartHP;
    }

    void Update() {

        if (hp > maxHP)
        {
            hp = maxHP;
        }

            if(hp <= 0 && gameObject.GetComponent<UpgradeManager>().lastChance == false)
        {
            hp = 0;
            alive = false;
        }
        else if (hp <= 0 && gameObject.GetComponent<UpgradeManager>().lastChance == true)
        {
            hp = 250;
            gameObject.GetComponent<TimeManager>().Slowmotion();
            gameObject.GetComponent<UpgradeManager>().lastChance = false;
        }
        
        if (hp <= 0 && saveScoreButton.GetBool("gameover") == false && gameObject.GetComponent<UpgradeManager>().lastChance == false){
            if(GetComponent<UpgradeManager>().theEnd == true)
            {
                ScoreManager.score = ScoreManager.score * 3;
            }
            gameObject.GetComponent<UpgradeManager>().normalizeBackdrop();
            saveScoreButton.SetBool("gameover", true);
            AudioManager.instance.playSound("GameOver");

            Destroy(spawner);
        }


        if (this.resetMenu.GetCurrentAnimatorStateInfo(0).IsName("down"))
        {
            SceneManager.LoadScene("Title Screen");
        }
    }
}
