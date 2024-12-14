using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //transform.rotation = Quaternion.Euler(2, 2, -2);
        //transform.LookAt(new Vector3(2, 2, -2));
        transform.rotation = Quaternion.LookRotation(new Vector3(2, 2, -2));
        Debug.Log(transform.rotation);
    }


}
