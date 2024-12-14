using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public LevelData currentLevelData;

    public static LevelLoader Instance { get; private set; }

    // Đảm bảo không có instance nào khác
    void Awake()
    {
        // Đảm bảo chỉ có một instance của GridManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại instance này khi chuyển scene
        }
        else
        {
            Destroy(gameObject); // Hủy đối tượng nếu đã có một instance
        }
    }

    public bool LoadLevel(int levelNumber)
    {
        string levelDataName = "LevelData" + levelNumber;
        currentLevelData = Resources.Load<LevelData>(levelDataName);

        if (currentLevelData != null)
        {
            // Debug.Log("Loaded " + currentLevelData.name);
            return true;
        }
        else
        {
            Debug.LogError("Không tìm thấy LevelData có tên: " + levelDataName);
            return false;
        }
    }

}