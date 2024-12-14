using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogNull : MonoBehaviour
{
    public string a = null;
    void Start()
    {
        Debug.Log(a == "");
    }


}
