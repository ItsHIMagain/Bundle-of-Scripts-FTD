using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    public bool reset;
    public Text curScore;

    private int upgradeCount = 1;
    void Start() {

        if(reset == true){
            PlayerPrefs.DeleteKey("Top1");
            PlayerPrefs.DeleteKey("Top2");
            PlayerPrefs.DeleteKey("Top3");
            PlayerPrefs.DeleteKey("Top4");
            PlayerPrefs.DeleteKey("Top5");

        PlayerPrefs.SetString("Top1Name", "---");
        PlayerPrefs.SetString("Top2Name", "---");
        PlayerPrefs.SetString("Top3Name", "---");
        PlayerPrefs.SetString("Top4Name", "---");
        PlayerPrefs.SetString("Top5Name", "---");
        }

        Debug.Log(PlayerPrefs.GetInt("Top1"));
        Debug.Log(PlayerPrefs.GetInt("Top2"));
        Debug.Log(PlayerPrefs.GetInt("Top3"));
        Debug.Log(PlayerPrefs.GetInt("Top4"));
        Debug.Log(PlayerPrefs.GetInt("Top5"));

        Debug.Log(PlayerPrefs.GetString("Top1Name"));
        Debug.Log(PlayerPrefs.GetString("Top2Name"));
        Debug.Log(PlayerPrefs.GetString("Top3Name"));
        Debug.Log(PlayerPrefs.GetString("Top4Name"));
        Debug.Log(PlayerPrefs.GetString("Top5Name"));
    }

    public void saveScore(){
        if(PlayerPrefs.GetInt("Top1") < score){
            PlayerPrefs.SetInt("Top5",PlayerPrefs.GetInt("Top4"));
            PlayerPrefs.SetInt("Top4",PlayerPrefs.GetInt("Top3"));
            PlayerPrefs.SetInt("Top3",PlayerPrefs.GetInt("Top2"));
            PlayerPrefs.SetInt("Top2",PlayerPrefs.GetInt("Top1"));
            PlayerPrefs.SetInt("Top1",score);

            PlayerPrefs.SetString("Top5Name", PlayerPrefs.GetString("Top4Name"));
            PlayerPrefs.SetString("Top4Name", PlayerPrefs.GetString("Top3Name"));
            PlayerPrefs.SetString("Top3Name", PlayerPrefs.GetString("Top2Name"));
            PlayerPrefs.SetString("Top2Name", PlayerPrefs.GetString("Top1Name"));
            PlayerPrefs.SetString("Top1Name", PlayerPrefs.GetString("SavedName"));

        } else if(PlayerPrefs.GetInt("Top2") < score && PlayerPrefs.GetInt("Top1") > score){
            PlayerPrefs.SetInt("Top5",PlayerPrefs.GetInt("Top4"));
            PlayerPrefs.SetInt("Top4",PlayerPrefs.GetInt("Top3"));
            PlayerPrefs.SetInt("Top3",PlayerPrefs.GetInt("Top2"));
            PlayerPrefs.SetInt("Top2",score);

            PlayerPrefs.SetString("Top5Name", PlayerPrefs.GetString("Top4Name"));
            PlayerPrefs.SetString("Top4Name", PlayerPrefs.GetString("Top3Name"));
            PlayerPrefs.SetString("Top3Name", PlayerPrefs.GetString("Top2Name"));
            PlayerPrefs.SetString("Top2Name", PlayerPrefs.GetString("SavedName"));

        } else if(PlayerPrefs.GetInt("Top3") < score && PlayerPrefs.GetInt("Top2") > score){
            PlayerPrefs.SetInt("Top5",PlayerPrefs.GetInt("Top4"));
            PlayerPrefs.SetInt("Top4",PlayerPrefs.GetInt("Top3"));
            PlayerPrefs.SetInt("Top3",score);

            PlayerPrefs.SetString("Top5Name", PlayerPrefs.GetString("Top4Name"));
            PlayerPrefs.SetString("Top4Name", PlayerPrefs.GetString("Top3Name"));
            PlayerPrefs.SetString("Top3Name", PlayerPrefs.GetString("SavedName"));

        } else if(PlayerPrefs.GetInt("Top4") < score && PlayerPrefs.GetInt("Top3") > score){
            PlayerPrefs.SetInt("Top5",PlayerPrefs.GetInt("Top4"));
            PlayerPrefs.SetInt("Top4",score);

            PlayerPrefs.SetString("Top5Name", PlayerPrefs.GetString("Top4Name"));
            PlayerPrefs.SetString("Top4Name", PlayerPrefs.GetString("SavedName"));

        } else if(PlayerPrefs.GetInt("Top5" ) < score && PlayerPrefs.GetInt("Top4") > score){
            PlayerPrefs.SetInt("Top5",score);
            PlayerPrefs.SetString("Top5Name", PlayerPrefs.GetString("SavedName"));
        }
    }

    private void Update()
    {
        
        curScore.text = score.ToString();
        
        if (score >= ((2500 * upgradeCount) + 500*(upgradeCount-1)) && upgradeCount <= (6+3) && gameObject.GetComponent<UpgradeManager>().isMidUpgrade == 0)
        {
            gameObject.GetComponent<UpgradeManager>().UpdateOptions();
            upgradeCount += 1;
        }
    }
}
