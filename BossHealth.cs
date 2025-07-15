using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 300f;
    public float currentHealth;
    public AudioSource deathsource;
    public AudioClip deathclip;
    public Slider healthSlider;
 

    public Animator animator;  // Assign in Inspector

    private bool isDead = false;

    void Start()
    {
        
        currentHealth = maxHealth;
    
    }
    private void Update()
    {
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;

        // Optional: play hurt animation here

        if (currentHealth <= 0)
        {
            Die();
            Sound();


        }
  
    }

    void Die()
    {
        isDead = true;

        if (animator)
        {
            animator.SetTrigger("Die");
            


        }
        StartCoroutine(WaitForDeathAnimation());
  
       
    }
    IEnumerator WaitForDeathAnimation()
    {
        // Assuming your death animation is 3 seconds long
        yield return new WaitForSeconds(5f);

        GameState.reachedBossFight = false; // full reset
        SceneManager.LoadScene("winscene");

        // Optional: destroy object after scene load, if persistent
        // Destroy(gameObject);
    }

    public void Sound() {
        if (deathsource && deathclip)
        {

            deathsource.PlayOneShot(deathclip);
        }
              

    }
 

}
