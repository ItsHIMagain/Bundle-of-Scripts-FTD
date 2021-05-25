using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateToMouse : MonoBehaviour
{
private Vector3 mousepos;
 public Transform target;
 private Vector3 objectpos;
 public float angle;
 
 void Update ()
 {
     mousepos = Input.mousePosition;
     mousepos.z = 5; //The distance between the camera and object
     objectpos = Camera.main.WorldToScreenPoint(target.position);
     mousepos.x = mousepos.x - objectpos.x;
     mousepos.y = mousepos.y - objectpos.y;
     angle = Mathf.Atan2(mousepos.y, mousepos.x) * Mathf.Rad2Deg;
     transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+270));
 }
}
