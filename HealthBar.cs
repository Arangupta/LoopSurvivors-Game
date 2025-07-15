using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image currentHealthBar;

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        currentHealthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}
