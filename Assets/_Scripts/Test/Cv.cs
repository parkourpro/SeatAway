using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cv : MonoBehaviour
{
    public GameObject gO;
    public Button btn;
    private RectTransform r;
    void Start()
    {
        r = gO.GetComponent<RectTransform>();
        btn.onClick.AddListener(LogSite);
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Debug.Log(Input.mousePosition);
    //    }
    //}

    void LogSite()
    {
        Debug.Log(r.anchoredPosition);
        //Debug.Log(r.anchorMax);
        //Debug.Log(r.pivot);
        //Debug.Log(r.offsetMin);
        //Vector2 a = gO.GetComponent<RectTransform>().anchorMax;
        //a.x += 0.05f;
        //gO.GetComponent<RectTransform>().anchorMax = a;

    }
}
