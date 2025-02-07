using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommingSoon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject commingSoonPanel;

    void Start()
    {
        commingSoonPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (commingSoonPanel.activeSelf)
            {
                commingSoonPanel.SetActive(false);
            }
        }
    }
}
