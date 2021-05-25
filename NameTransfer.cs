using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NameTransfer : MonoBehaviour
{
    private string theName;
    public static bool nameSet = false;
    public GameObject inputField;
    public GameObject manager;
    public Animator anim;
    public Animator startScreen;
    public GameObject scoreManager;
    public Text curScore;
    public GameObject explosion;

    public void StoreName(){
            if(anim.GetBool("nameEntered") == false){
            theName = inputField.GetComponent<Text>().text;
            PlayerPrefs.SetString("SavedName", theName);
            nameSet = true;
            manager.GetComponent<ScoreManager>().saveScore();
            anim.SetBool("nameEntered", true);
            scoreManager.GetComponent<HighscoreManager>().OnUpdates();

            if (manager.GetComponent<UpgradeManager>().theEnd == true)
            {
                SceneManager.LoadScene("The End");
            } else if (manager.GetComponent<UpgradeManager>().corruptionCounter != 0)
            {
                startScreen.SetBool("gameEnded", true);
            } else
            {
                startScreen.SetBool("gameEnded", true);
            }
            Instantiate(explosion, curScore.transform.position, Quaternion.identity);
            Destroy(curScore);
        }
    }
}
