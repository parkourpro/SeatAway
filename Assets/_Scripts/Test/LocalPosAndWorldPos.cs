using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPosAndWorldPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(transform.position+ "w");
            Debug.Log(transform.localPosition + "l");
        }
    }
}
