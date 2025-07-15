using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeMac : MonoBehaviour
{
    public float loopDuration = 60f;
    private float timer;
    public float RemainingTime => timer;

    public Vector3 startingPoint;
    public GameObject player;
    public EnemySpawner spawner;
    public PowerUpSpawner powerUpSpawner;
    public int loopCount = 0;
    public int maxloop = 3;

    public AudioSource audioSource;
    public AudioClip generalLoopClip;
    public AudioClip BossClip;
    public AudioClip[] roundClips;  // Round 1, 2, 3 audio

    [SerializeField] private Animator animator;

    private bool isLooping = false;
    private bool secondWaveSpawned = false;

    private IEnumerator Start()
    {
        startingPoint = player.transform.position;
        timer = loopDuration;

        Time.timeScale = 1;

        // Play Round 1 intro sound
        if (roundClips.Length > 0 && roundClips[0] != null)
        {
            audioSource.PlayOneShot(roundClips[0]);
            yield return new WaitForSeconds(roundClips[0].length);
        }

        // First wave
        spawner.StartWave();
        powerUpSpawner.SpawnPowerUps();

        player.transform.position = startingPoint;
        timer = loopDuration;

        yield return new WaitForSeconds(0.5f);

        if (audioSource && generalLoopClip)
            audioSource.PlayOneShot(generalLoopClip);

        loopCount++;
    }

    void Update()
    {
        if (isLooping) return;

        timer -= Time.deltaTime;

        //Spawn second wave 30s after the loop starts
        if (!secondWaveSpawned && timer <= loopDuration - 30f)
        {
            secondWaveSpawned = true;
            Debug.Log("Timer reached 30s — spawning second wave.");
            spawner.StartWave();
        }

        if (timer <= 0f)
        {
            isLooping = true;

            if (loopCount >= maxloop)
            {
                StartCoroutine(BossLevelTransition());
            }
            else
            {
                StartCoroutine(LoopTransition());
            }
        }
    }

    private IEnumerator BossLevelTransition()
    {
        animator?.SetTrigger("End");
        audioSource.PlayOneShot(BossClip);

        // Optional cleanup if boss loads same session
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("BossINtro");
    }

    private IEnumerator LoopTransition()
    {
        Debug.Log($"Starting Loop {loopCount + 1}");

        if (loopCount > 0 && animator != null)
        {
            animator.SetTrigger("End");
        }

        // Play round intro clip
        if (loopCount < roundClips.Length && roundClips[loopCount] != null)
        {
            audioSource.PlayOneShot(roundClips[loopCount]);
            yield return new WaitForSeconds(roundClips[loopCount].length);
        }

        // Clean enemies and powerups
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        foreach (GameObject health in GameObject.FindGameObjectsWithTag("healthboost"))
            Destroy(health);

        // Reset for new loop
        spawner.StartWave();
        powerUpSpawner.SpawnPowerUps();
        player.transform.position = startingPoint;
        timer = loopDuration;
        secondWaveSpawned = false;

        yield return new WaitForSeconds(0.5f);

        if (audioSource && generalLoopClip)
            audioSource.PlayOneShot(generalLoopClip);

        loopCount++;
        isLooping = false;
    }
}
