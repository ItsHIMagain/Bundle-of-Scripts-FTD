using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public Animator anim;
   public void ChangeScene(int screneToChangeTo) {
       if(screneToChangeTo == 1){
            anim.SetBool("HasStart", true);
        }
   }

    private void Update()
    {
        if(this.anim.GetCurrentAnimatorStateInfo(0).IsName("IsUpSlide"))
        {
    StatManager.hp = 10;
    StatManager.alive = true;
    SceneManager.LoadScene("Intro");
        }
    }
}
