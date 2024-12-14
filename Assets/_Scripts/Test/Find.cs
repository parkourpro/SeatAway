using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find : MonoBehaviour
{
    public GameObject parent;
    void Start()
    {
        Transform b = parent.transform.Find("b");
        if (b != null)
        {
            Debug.Log("found b");
        }
        Transform a = parent.transform.Find("a");
        if (a != null)
        {
            Debug.Log("found a");
        }
    }

 
}
