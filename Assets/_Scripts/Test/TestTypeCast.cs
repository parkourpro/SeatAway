using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTypeCast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log((2*3).GetType());
        Debug.Log((2*3f).GetType());
        Debug.Log((2*3.1f).GetType());
        Debug.Log((2+3).GetType());
        Debug.Log((2+3f).GetType());
        Debug.Log((2/3f).GetType());
        Debug.Log((2 -3f).GetType());
    }

   
}
