using UnityEngine;

public class Child : MonoBehaviour
{
    public GameObject g;
    void Start()
    {
        GameObject p = GameObject.Find("p");

        foreach (Transform c in p.transform)
        {
            Debug.Log(c.name);
        }
    }
}