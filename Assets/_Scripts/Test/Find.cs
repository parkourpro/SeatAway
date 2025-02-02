using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find : MonoBehaviour
{
    public GameObject parent;
    void Start()
    {
        //Transform b = parent.transform.Find("b");
        //if (b != null)
        //{
        //    Debug.Log("found b");
        //}
        //Transform a = parent.transform.Find("a");
        //if (a != null)
        //{
        //    Debug.Log("found a");
        //}
        GameObject myObject = GameObject.Find("Parent");
        GameObject myObject2 = GameObject.Find("a");
        if (myObject != null)
        {
            Debug.Log("found Parent");
        }
        if (myObject2 != null)
        {
            Debug.Log("found a");
        }
    }
}
