using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DateTime now = DateTime.Now;

        // In ra thời gian hiện tại
        string time1 = now.ToString();
        //Debug.Log(now.ToString());

        string time2 = "2/14/2025 2:15:30 AM";
        int differenceInSeconds = CalculateTimeDifferenceInSeconds(time1, time2);
        Debug.Log("Hiệu số giây: " + differenceInSeconds);
    }

    // Update is called once per frame
    int CalculateTimeDifferenceInSeconds(string time1, string time2)
    {
        // Định dạng của chuỗi thời gian
        string format = "M/d/yyyy h:mm:ss tt";

        // Chuyển đổi chuỗi thành DateTime
        DateTime dateTime1 = DateTime.ParseExact(time1, format, System.Globalization.CultureInfo.InvariantCulture);
        DateTime dateTime2 = DateTime.ParseExact(time2, format, System.Globalization.CultureInfo.InvariantCulture);

        // Tính hiệu giữa hai thời điểm
        TimeSpan difference = dateTime1 - dateTime2;

        // Trả về tổng số giây
        return (int)difference.TotalSeconds;
    }
}
