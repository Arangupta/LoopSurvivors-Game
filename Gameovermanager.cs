using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameovermanager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    private CameraFall cameraFall;

    void Start()
    {
        cameraFall = Camera.main.GetComponent<CameraFall>();
    }

    public void PlaySound()
    {
        if (audioSource && audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        if (cameraFall != null)
            cameraFall.ResetFall();

        if (GameState.reachedBossFight)
        {
            SceneManager.LoadScene("BossFight"); // Restart from boss
        }
        else
        {
            SceneManager.LoadScene("gamescene"); // Restart from beginning
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        if (cameraFall != null)
            cameraFall.ResetFall();

        GameState.reachedBossFight = false; // Reset flag
        SceneManager.LoadScene("main menu");
    }
}
