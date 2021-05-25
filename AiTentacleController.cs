using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTentacleController : MonoBehaviour
{
    /*
     public float speed = 1.5f;
     private Vector3 target = new Vector3(0,0,0);
     
     void Update () {
                GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        if(gos != null){
         transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, speed * Time.deltaTime);
        }
     }
     */
}
