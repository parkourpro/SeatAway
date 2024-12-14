using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNum : MonoBehaviour
{
    public float d = 5.1f;
    void Start()
    {
        int a = 5;
        int b = 10;
        int c = RoundToRange(d, a, b);
        Debug.Log(c);
    }

    int RoundToRange(float value, int a, int b)
    {
        int roundedValue = Mathf.RoundToInt(value);
        return Mathf.Clamp(roundedValue, a, b);
    }
}
