using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    public AudioSource source;
    public AudioClip clip;
    public void sound()
    {

        if (source && clip)
        {

            source.PlayOneShot(clip);
        }
    }

    public void MainMenu()
    {
        
        SceneManager.LoadScene("main menu"); //  Load Main Menu scene
    }
}