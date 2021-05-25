using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    public static bool pressed = false;
    public float speed;
    private Vector3 target;

         void Start () {
         target = transform.position;
     }
     
     void Update () {
         if(pressed == true){
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
         }
     } 
}
