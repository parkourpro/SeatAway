using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public delegate void MyDelegate();
    public static MyDelegate startTimeDelegate;
    public static MyDelegate stopTimeDelegate;

    private GridManager gridManager;
    public BusController busController;

    private int timeOfLevel;
    public Image green;
    public TextMeshProUGUI timeShow;
    private int remainSecond;
    private bool hasWon = false;
    private bool isSetUpTime = false;
    private bool isDraggingSeat = false;
    private float startTime;
    private float elapseTime;
    private bool isHandlingLose = false;
    IEnumerator Start()
    {
        gridManager = GridManager.Instance;
        yield return new WaitUntil(() => gridManager.doneGrid != "");
        timeOfLevel = gridManager.levelData.timeInSecond;
        isSetUpTime = true;
    }

    private void OnEnable()
    {
        startTimeDelegate += StartGame;
        stopTimeDelegate += StopTime;

    }
    private void OnDisable()
    {
        startTimeDelegate -= StartGame;
        stopTimeDelegate -= StopTime;
    }


    void Update()
    {
        if (isSetUpTime)
        {
            if (hasWon || isHandlingLose) return;
            if (!isDraggingSeat)
            {
                startTime = Time.timeSinceLevelLoad;
                remainSecond = timeOfLevel;
            }
            else
            {
                elapseTime = Time.timeSinceLevelLoad - startTime;
                float elapsedRatio = elapseTime / timeOfLevel;

                if (elapsedRatio < 0.5f)
                {
                    // Tăng r từ 0 lên 255 khi tỷ lệ từ 0 đến 0.5
                    float r = Mathf.Lerp(0, 255, elapsedRatio * 2);
                    green.color = new Color(r / 255f, 1f, 0f);
                }
                else
                {
                    // Giảm g từ 255 xuống 0 khi tỷ lệ từ 0.5 đến 1
                    float g = Mathf.Lerp(1f, 0f, (elapsedRatio - 0.5f) * 2);
                    green.color = new Color(1f, g, 0f);
                }

                green.fillAmount = (float)(1 - elapseTime / timeOfLevel);


                remainSecond = timeOfLevel - (int)elapseTime;
                if (remainSecond < 0)
                {
                    remainSecond = 0;
                    //yield return StartCoroutine(busController.CloseDoor());
                    //CanvasController.onLoseDo?.Invoke();
                    StartCoroutine(HandleLose());
                }
            }


            timeShow.text = FormatTime(remainSecond);
        }
    }


    void StartGame()
    {
        isDraggingSeat = true;
    }

    void StopTime()
    {
        hasWon = true;
    }

    string FormatTime(int seconds)
    {
        int minutes = seconds / 60;
        seconds %= 60;

        string formattedMinutes = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        string formattedSeconds = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();

        return $"{formattedMinutes}:{formattedSeconds}";
    }

    private IEnumerator HandleLose()
    {
        isHandlingLose = true; // Đặt cờ để ngăn hàm update chạy
        yield return StartCoroutine(busController.CloseDoor()); // Đợi đóng cửa xong
        CanvasController.onLoseDo?.Invoke(); // Sau khi cửa đóng, gọi hành động thua
    }

    public void AddTime(int additionalSeconds)
    {
        if (!isSetUpTime || hasWon || isHandlingLose) return;

        if (Time.timeSinceLevelLoad - startTime >= additionalSeconds)
        {
            startTime += additionalSeconds;
        }
        else
        {
            startTime = Time.timeSinceLevelLoad;
        }

    }


}
