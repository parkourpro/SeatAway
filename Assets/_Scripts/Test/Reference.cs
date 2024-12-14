using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    public Transform transforma;

    private void Start()
    {
        transforma = gameObject.GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 a = Vector3.up;
            transforma.position += a*3;
        }
    }
}
