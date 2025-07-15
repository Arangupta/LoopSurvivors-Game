using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunReload : MonoBehaviour
{
    [Header("Ammo Settings")]
    public int maxAmmo = 30;
    public float reloadTime = 2f;

    [Header("UI")]
    public Image ammoReloadBar;  // Single UI image for ammo and reload
    [Header("ReloadSound")]
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    private int currentAmmo;
    private bool isReloading = false;

    public int CurrentAmmo => currentAmmo;
    public bool IsReloading => isReloading;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateUIFill();
    }

    void Update()
    {
        if (isReloading)
            return;

        // Auto reload if ammo empty
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    public bool TryUseAmmo()
    {
        if (isReloading || currentAmmo <= 0)
            return false;

        currentAmmo--;
        UpdateUIFill();

        if (currentAmmo <= 0)
        {
            // Start reload immediately if ammo empty after shot
            StartCoroutine(Reload());
        }

        return true;
    }

    IEnumerator Reload()
    {

        if (AudioSource && AudioClip)
        {

            AudioSource.PlayOneShot(AudioClip);

        }
        isReloading = true;
        float elapsed = 0f;

        while (elapsed < reloadTime)
        {
            elapsed += Time.deltaTime;
            ammoReloadBar.fillAmount = Mathf.Lerp(0, 1, elapsed / reloadTime);
            yield return null;
        }

        currentAmmo = maxAmmo;
        isReloading = false;
        ammoReloadBar.fillAmount = 1f;
    }

    private void UpdateUIFill()
    {
        ammoReloadBar.fillAmount = (float)currentAmmo / maxAmmo;
    }
}
