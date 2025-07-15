using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip AudioClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void sound()
    {
        if (AudioSource && AudioClip)
        {

            AudioSource.PlayOneShot(AudioClip);

        }
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Storyline");
        if (PlayerPrefs.GetInt("Storyline", 0) == 0)
        {
            PlayerPrefs.SetInt("Storyline", 1);
            SceneManager.LoadScene("Storyline");
        }
        else
        {
            SceneManager.LoadScene("gamescene");
        }
    }


    public void QuitGame()
    {
        Debug.Log("Game Closed!");
        Application.Quit(); // Close the game (only works in a built version)
    }
}
