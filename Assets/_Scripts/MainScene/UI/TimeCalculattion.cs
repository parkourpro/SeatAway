using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCalculattion
{
    public static int CalculateTimeDifferenceInSeconds(string time1, string time2)
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

    public static string ConvertSecondsToMinutesSeconds(int totalSeconds)
    {
        // Tính số phút và số giây
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        // Định dạng chuỗi với 2 chữ số cho phút và giây
        string timeString = $"{minutes:D2}:{seconds:D2}";

        return timeString;
    }
}
