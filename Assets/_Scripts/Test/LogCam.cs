using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCam : MonoBehaviour
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
            //Debug.Log(Camera.main.WorldToScreenPoint(transform.position));
            Vector3 screenPosition = Input.mousePosition; // Lấy tọa độ chuột trên màn hình
            screenPosition.z = 12f;
            Debug.Log(Camera.main.ScreenToWorldPoint(screenPosition));
            //Debug.Log(Input.mousePosition);
            //Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            //Debug.Log(worldPosition);

        }
    }
}
