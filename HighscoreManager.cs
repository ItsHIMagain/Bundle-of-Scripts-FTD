using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public Text top1;
    public Text top2;
    public Text top3;
    public Text top4;
    public Text top5;

    public Text Top1Name;
    public Text Top2Name;
    public Text Top3Name;
    public Text Top4Name;
    public Text Top5Name;
    void Start()
    {
        top1.text = PlayerPrefs.GetInt("Top1").ToString();
        top2.text = PlayerPrefs.GetInt("Top2").ToString();
        top3.text = PlayerPrefs.GetInt("Top3").ToString();
        top4.text = PlayerPrefs.GetInt("Top4").ToString();
        top5.text = PlayerPrefs.GetInt("Top5").ToString();
        Top1Name.text = PlayerPrefs.GetString("Top1Name");
        Top2Name.text = PlayerPrefs.GetString("Top2Name");
        Top3Name.text = PlayerPrefs.GetString("Top3Name");
        Top4Name.text = PlayerPrefs.GetString("Top4Name");
        Top5Name.text = PlayerPrefs.GetString("Top5Name");
    }

    public void OnUpdates(){
        top1.text = PlayerPrefs.GetInt("Top1").ToString();
        top2.text = PlayerPrefs.GetInt("Top2").ToString();
        top3.text = PlayerPrefs.GetInt("Top3").ToString();
        top4.text = PlayerPrefs.GetInt("Top4").ToString();
        top5.text = PlayerPrefs.GetInt("Top5").ToString();
        Top1Name.text = PlayerPrefs.GetString("Top1Name");
        Top2Name.text = PlayerPrefs.GetString("Top2Name");
        Top3Name.text = PlayerPrefs.GetString("Top3Name");
        Top4Name.text = PlayerPrefs.GetString("Top4Name");
        Top5Name.text = PlayerPrefs.GetString("Top5Name");
    }
}
