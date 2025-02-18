using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/playerData.json";

    // Lưu toàn bộ dữ liệu vào file JSON
    private static void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        //Debug.Log("Data saved to " + path);
    }

    // Đọc toàn bộ dữ liệu từ file JSON
    private static PlayerData LoadData()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            //Debug.Log("Data loaded from " + path);
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path + ", create new json file");
            PlayerData data = new PlayerData();
            data.coins = -1;
            data.heart = -1;
            data.lastTimeLose = DateTime.Now.ToString();
            data.remainderTimeSec = -1;
            data.volumn = -1;
            SaveData(data);

            return data; // Trả về dữ liệu mặc định nếu file không tồn tại
        }
    }

    // Lưu second
    public static void SaveCoin(int coin)
    {
        PlayerData data = LoadData(); // Đọc dữ liệu hiện có
        data.coins = coin; // Cập nhật giá trị second
        SaveData(data); // Lưu lại toàn bộ dữ liệu
    }

    // Lấy second
    public static int GetCoin()
    {
        PlayerData data = LoadData(); // Đọc dữ liệu hiện có
        return data.coins; // Trả về giá trị second
    }

    // Lưu heart
    public static void SaveHeart(int heart)
    {
        PlayerData data = LoadData(); // Đọc dữ liệu hiện có
        data.heart = heart; // Cập nhật giá trị heart
        SaveData(data); // Lưu lại toàn bộ dữ liệu
    }

    // Lấy heart
    public static int GetHeart()
    {
        PlayerData data = LoadData(); // Đọc dữ liệu hiện có
        return data.heart; // Trả về giá trị heart
    }

    // Lưu lastTimeLose
    public static void SaveTime()
    {
        string time = DateTime.Now.ToString();
        PlayerData data = LoadData(); // Đọc dữ liệu hiện có
        data.lastTimeLose = time; // Cập nhật giá trị lastTimeLose
        SaveData(data); // Lưu lại toàn bộ dữ liệu
    }

    // Lấy lastTimeLose
    public static string GetTime()
    {
        PlayerData data = LoadData(); // Đọc dữ liệu hiện có
        return data.lastTimeLose; // Trả về giá trị lastTimeLose
    }

    public static void SaveRemainderSec(int second)
    {
        PlayerData data = LoadData(); // Đọc dữ liệu hiện có
        data.remainderTimeSec = second; // Cập nhật giá trị second
        SaveData(data); // Lưu lại toàn bộ dữ liệu
    }

    // Lấy second
    public static int GetRemainderSec()
    {
        PlayerData data = LoadData(); // Đọc dữ liệu hiện có
        return data.remainderTimeSec; // Trả về giá trị second
    }

    // Lưu volumn
    public static void SaveVolumn(float volumn)
    {
        PlayerData data = LoadData(); // Đọc dữ liệu hiện có
        data.volumn = volumn; // Cập nhật giá trị volumn
        SaveData(data); // Lưu lại toàn bộ dữ liệu
    }

    // Lấy volumn
    public static float GetVolumn()
    {
        PlayerData data = LoadData(); // Đọc dữ liệu hiện có
        return data.volumn; // Trả về giá trị volumn
    }
}