using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour
{
    
    public Transform bar;
    public float dif;
    public float width;

    private void Update() {
        if(StatManager.hp <= StatManager.maxHP){
            bar.localScale = new Vector3(StatManager.hp + dif, width);
        } else {
            bar.localScale = new Vector3(StatManager.maxHP + dif, width);
        }
    }
}
