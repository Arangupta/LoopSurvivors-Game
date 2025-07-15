using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerhealthsystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public Animator animator;
    public CameraFall cameraFall;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private AudioSource DeathSound;
    [SerializeField] private AudioClip DeathSoundClip;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(DieAnimation());
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private IEnumerator DieAnimation()
    {
        isDead = true;

        if (cameraFall != null)
        {
            cameraFall.StartCameraFall();
            DeathSound.PlayOneShot(DeathSoundClip);
        }
        if (animator != null)
        {
            animator.SetTrigger("End");
        }
        else
        {
            Debug.LogWarning("Fade Animator not assigned.");
        }


        yield return new WaitForSeconds(1.5f);
        Die();
    }

    void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("GameOver");
        Debug.Log("Player Died!");
        gameObject.SetActive(false);
    }
}
