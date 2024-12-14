using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    public float forceAmount = 2f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Áp dụng lực theo chế độ "Force"
        //rb.AddForce(Vector3.up * forceAmount, ForceMode.Force);

        //// Áp dụng lực theo chế độ "Acceleration"
        rb.AddForce(Vector3.forward * forceAmount, ForceMode.Acceleration);

        //// Áp dụng lực theo chế độ "Impulse"
        //rb.AddForce(Vector3.forward * forceAmount, ForceMode.Impulse);

        // Thay đổi vận tốc trực tiếp theo chế độ "VelocityChange"
        //rb.AddForce(Vector3.forward * forceAmount, ForceMode.VelocityChange);
    }

}
