using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneLoader : MonoBehaviour
{
    [SerializeField] string videofilename;

    public string nextSceneName;

    void Start()
    {
        VideoPlayer();
    }

  
    public void VideoPlayer() { 
         VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer) {
            string videopath = System.IO.Path.Combine(Application.streamingAssetsPath, videofilename);
            Debug.Log(videopath);
            videoPlayer.url = videopath;
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play(); 
        
        }
    
    }
    void OnVideoEnd(VideoPlayer vp)
    {
    GameState.reachedBossFight = true; // mark boss scene
    SceneManager.LoadScene(nextSceneName);
    }


}
