using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [Tooltip("Reference to the TimeMac script in the scene")]
    public TimeMac timeMac;                 // drag your TimeMac GO here
    public TextMeshProUGUI timerText;       // drag the TMP Text here

    void Update()
    {
        if (timeMac == null) return;

        float remaining = Mathf.Max(0f, timeMac.RemainingTime);
        int seconds = Mathf.CeilToInt(remaining);
        timerText.text = $"Time Left: {seconds}s";
    }
}
