using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMousePos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

     void OnMouseDown()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Debug.Log(transform.position);
    }
    private void OnMouseUp()
    {

    }

    private void OnMouseDrag()
    {
        
    }
}
