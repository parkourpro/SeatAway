using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Kéo VideoPlayer vào đây
    public Button playButton;       // Kéo Button UI vào đây (nếu cần)

    void Start()
    {
        // Đảm bảo các nút có sự kiện
        if (playButton != null)
            playButton.onClick.AddListener(PlayVideo);

        // VideoPlayer không tự chạy
        //videoPlayer.playOnAwake = false;
    }

    void PlayVideo()
    {
        videoPlayer.Play();
    }

}
