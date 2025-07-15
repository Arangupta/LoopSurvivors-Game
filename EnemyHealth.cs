using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemyhelth : MonoBehaviour
{
    public float EnemyHelth = 100f;
    public Slider healthSlider; // Assign in Inspector
    public CanvasGroup healthCanvasGroup; // Assign in Inspector

    public float fadeDelay = 2f;
    public float fadeDuration = 1f;

    private Coroutine fadeCoroutine;

    void Start()
    { 
       healthSlider.value = EnemyHelth;
        healthCanvasGroup.alpha = 0f; // Hidden initially
    }

    public void TakeDamage(float amount)
    {
        EnemyHelth -= amount;
        healthSlider.value = EnemyHelth;

        // Show health bar
        healthCanvasGroup.alpha = 1f;

        // Restart fade coroutine
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeHealthBar());

        if (EnemyHelth <= 0)
        {
            Die();
        }
    }

    IEnumerator FadeHealthBar()
    {
        yield return new WaitForSeconds(fadeDelay);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            healthCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        healthCanvasGroup.alpha = 0f;
    }

    void Die()
    {
        Destroy(gameObject); // Destroys the enemy
    }
}
