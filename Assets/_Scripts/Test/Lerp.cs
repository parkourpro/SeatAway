using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lerp : MonoBehaviour
{

    public float speed = 5f;
    public GameObject white;
    public GameObject gray;
    private Vector3 start;
    private Vector3 end;
    // Start is called before the first frame update
    void Start()
    {
        start = gray.transform.position;
        end = white.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > 1)
        {
            gray.transform.position = Vector3.Lerp(gray.transform.position, end, Time.deltaTime * speed);
            if(Vector3.Distance(gray.transform.position, end) > 0.1f)
            {
                Debug.Log(1);
            }
        }
    }
}
