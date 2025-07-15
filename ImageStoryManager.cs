using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageStoryManager : MonoBehaviour
{
    public Image storyImage;
    public Sprite[] storySprites;
    private int currentIndex = 0;
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    void Start()
    {
        storyImage.sprite = storySprites[currentIndex];
        if (AudioSource && AudioClip)
        {

            AudioSource.PlayOneShot(AudioClip);

        }
    
    }

    public void ShowNextImage()
    {
        currentIndex++;
        if (currentIndex < storySprites.Length)
        {
            storyImage.sprite = storySprites[currentIndex];
        }
        else
        {
            SceneManager.LoadScene("gamescene");
        }
    }
}
